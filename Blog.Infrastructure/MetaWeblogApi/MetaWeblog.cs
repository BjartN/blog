using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Blog.Core;
using Blog.Infrastructure.MetaWeblogApi.Entities;
using Post=Blog.Infrastructure.MetaWeblogApi.Entities.Post;

namespace Blog.Infrastructure.MetaWeblogApi
{
    public class MetaWeblog : IMetaWeblog
    {
        private readonly IUrlContext _urlContext;
        private readonly IAuthenticationService _authenticationService;
        private readonly BlogSettings _settings;
        private readonly IRepository _repository;

        public MetaWeblog(IRepository repository, IUrlContext urlContext, IAuthenticationService authenticationService)
        {
            _repository = repository;
            _urlContext = urlContext;
            _authenticationService = authenticationService;
            _settings = BlogSettings.Get(_repository);
        }

        public object EditPost(string postid, string username, string password, Post sourcePost, bool publish)
        {
            validateRequest(username, password);

            var post = Core.Post.GetPost(postid, _repository);

						if (post == null)
							throw new NoSuchPostException(null,postid);

            post.UpdatePost(
                sourcePost.title,
                sourcePost.description,
                true,
                username,
                sourcePost.categories.Select(x => new Tag(x)).ToList()
                );

            Core.Post.Save(post, _repository);

            return true;
        }

        public CategoryInfo[] GetCategories(string blogid, string username, string password)
        {
            return Core.Post.GetTags(_repository).Select(x => new CategoryInfo
            {
                title = x.TagName, 
                description = x.TagName, 
                categoryid = x.TagName, 
                htmlUrl = "http://bjarte.com", 
                rssUrl = "http://bjarte.com/rss"
            }).ToArray();
        }

        public Post GetPost(string postid, string username, string password)
        {
            validateRequest(username, password);

            var targetPost = new Post();
            var sourcePost = Core.Post.GetPost(postid, _repository);

						if (sourcePost == null)
							throw new NoSuchPostException(null, postid);

            targetPost.postid = sourcePost.Id;
            targetPost.dateCreated = sourcePost.Created;
            targetPost.title = sourcePost.Title;
            targetPost.description = sourcePost.Body;
            targetPost.link = _urlContext.GetPostUrl(sourcePost);
            targetPost.mt_allow_comments = "0"; // or "1"
            targetPost.userid = sourcePost.Author;
            targetPost.categories = sourcePost.Tags.Select(x => x.TagName).ToArray();

            return targetPost;
        }

        public Post[] GetRecentPosts(string blogid, string username, string password, int numberOfPosts)
        {
            validateRequest(username, password);

            var sendPosts = new List<Post>();
            var posts = Core.Post.GetPublishedPosts(_repository).Take(50).ToList();

            // Set End Point
            var stop = numberOfPosts;
            if (stop > posts.Count)
                stop = posts.Count;

            foreach (var post in posts.GetRange(0, stop))
            {

                var tempPost = new Post
                {
                    postid = post.Id,
                    dateCreated = post.Created,
                    title = post.Title,
                    description = post.Body,
                    link = _urlContext.GetPostUrl(post),
                    categories = post.Tags.Select(x => x.TagName).ToArray()
                };

                sendPosts.Add(tempPost);
            }

            return sendPosts.ToArray();
        }

        public string NewPost(string blogid, string username, string password, Post sourcePost, bool publish)
        {
            validateRequest(username, password);

            var post = Core.Post.CreatePost(
                sourcePost.title,
                sourcePost.description,
                username,
                sourcePost.categories.Select(x => new Tag(x)).ToList()
                );

            Core.Post.Save(post, _repository);

            return post.Id;
        }

        public UrlData NewMediaObject(string blogid, string username, string password, FileData mediaObject)
        {
            validateRequest(username, password);

            var mediaInfo = new UrlData();

            //Not loving this code...
            var absoluteRootDirectory = _urlContext.MapPath(_settings.VirtualMediaPath);
            var absoluteFilePath = toUniqueFileName(Path.Combine(absoluteRootDirectory, mediaObject.name.Replace('/', Path.DirectorySeparatorChar)));
            var absoluteFileDirectory = Path.GetDirectoryName(absoluteFilePath);

            createDirectory(absoluteFileDirectory);
            persistFile(mediaObject, absoluteFilePath);

            mediaInfo.url = _urlContext.AbsoluteUrlWithDomain(_settings.VirtualMediaPath + "/" + mediaObject.name);

            return mediaInfo;
        }

        public BlogInfo[] GetUsersBlogs(string appKey, string username, string password)
        {
            var blogs = new List<BlogInfo>();

            validateRequest(username, password);

            var temp = new BlogInfo { url = "", blogid = "0", blogName = _settings.Title };
            blogs.Add(temp);

            return blogs.ToArray();
        }

        public bool DeletePost(string appKey, string postid, string username, string password, bool publish)
        {
            validateRequest(username, password);
            try
            {
                Core.Post.Delete(postid, _repository);
            }
            catch (Exception ex)
            {
                throw new MetaWeblogException(ex);
            }

            return true;
        }

        private static void createDirectory(string absoluteDirectory)
        {
            if (!Directory.Exists(absoluteDirectory))
                Directory.CreateDirectory(absoluteDirectory);
        }

        private static void persistFile(FileData mediaObject, string absoluteFilePath)
        {
            var fileStream = new FileStream(absoluteFilePath, FileMode.Create);
            var binaryWriter = new BinaryWriter(fileStream);
            binaryWriter.Write(mediaObject.bits);
            binaryWriter.Close();
        }

        private static string toUniqueFileName(string absoluteFilePath)
        {
            if (File.Exists(absoluteFilePath))
            {
                var originalFile = Path.GetFileNameWithoutExtension(absoluteFilePath);
                var extention = Path.GetExtension(absoluteFilePath);

                for (int count = 1; count < 30000; count++)
                {
                    var newFile = string.Format("{0}({1}).{2}", originalFile, count, extention);
                    if (!File.Exists(newFile))
                        return Path.Combine(Path.GetDirectoryName(absoluteFilePath), newFile);
                }
            }
            return absoluteFilePath;
        }

        private void validateRequest(string userName, string password)
        {
            if (!_authenticationService.Authenticate(userName, password, false))
            {
                throw new MetaWeblogException(null);
            }
        }
    }

    public class MetaWeblogException : Exception
    {
        public MetaWeblogException(Exception inner): base("MetaWeblogError", inner)
        {
        }
    }
}