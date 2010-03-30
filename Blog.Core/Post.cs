using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Blog.Core
{
    [Serializable]
    public class Post
    {
        public Post()
        {
            Tags = new List<Tag>();
        }

        public DateTime Created { get; private set; }
        public string LegacyUrl { get; private set; }
        
        public string Id { get; private set; }
        public string Title { get; private set; }
        public string Body { get; private set; }
        public string Slug { get; private set; }
        public string Author { get; private set; }
        public bool IsPublished { get; private set; }
        public IList<Tag> Tags { get; private set; }

        public static IQueryable<Post> GetPublishedPosts(IRepository repository)
        {
            //TODO: Pulling every single post out of the db are we ?
            return repository.List<Post>()
                .Where(x => x.IsPublished).ToList()
                .OrderByDescending(x => x.Created).AsQueryable();
        }

        public static Post GetPostForDisplay(IRepository repository, string id)
        {
            return repository.Get<Post>(id);
            //return repository.List<Post>().Where(x => x.IsPublished && x.Id.ToLower() == id.ToLower()).SingleOrDefault();
        }

        public static IQueryable<Post> GetArchive(IRepository repository)
        {
            return GetPublishedPosts(repository);
        }

        public static IQueryable<Post> GetPostsByTag(string tag, IRepository repository)
        {
            return GetPublishedPosts(repository).Where(x => x.Tags.Contains(new Tag(tag)));
        }

        public static Post GetPostsBySlug(string slug, IRepository repository)
        {
            return GetPublishedPosts(repository)
                .Where(x=>x.Slug == slug.ToLower()).SingleOrDefault();
        }

        public void SetTags(IList<Tag> tags)
        {
            Tags = tags;
        }

        public static void Save(Post post, IRepository repository)
        {
            repository.Save(post);
        }

        public static Post GetPost(string id, IRepository repository)
        {
            return repository.Get<Post>(id);
        }

        public static void Delete(string id, IRepository repository)
        {
            var post = GetPost(id, repository);
            repository.Delete(post);
        }

        public static IQueryable<Tag> GetTags(IRepository repository)
        {
            //TODO: Pulling every single post out of the db are we ?
            var posts = repository.List<Post>().ToList();
            var tags = new List<Tag>();
            foreach(var post in posts)
            {
                tags = tags.Union(post.Tags).Distinct().ToList();
            }
            return tags.AsQueryable();
        }

        public void UpdatePost(string title, string body, bool isPublished, string userName, IList<Tag> tags)
        {
            if (tags == null)
                tags = new List<Tag>();

            Author = userName;
            Title = title;
            Body = body;
            IsPublished = isPublished;
            Tags = tags;
        }

        public static Post CreatePost(string title, string body, string userName, IList<Tag> tags)
        {
            if(tags==null)
                tags= new List<Tag>();

            return new Post
            {
                Author = userName,
                Title = title,
                Body = body,
                IsPublished = true,
                Slug = toSlug(title),
                Id = Guid.NewGuid().ToString(),
                Created= DateTime.Now,
                Tags = tags
            };
        }

        public static Post CreateLegacyPost(string title, string body,  string userName, IList<Tag> tags, DateTime created, string legacyUrl, string legacyId)
        {
            var post = CreatePost(title, body, userName, tags);
            post.LegacyUrl = legacyUrl;
            post.Created = created;
            post.Id = legacyId;
            return post;
        }

        public static string toSlug(string phrase)
        {
           
            var str =phrase.ToLower();

            str = Regex.Replace(str, @"[^a-z0-9\s-]", ""); // invalid chars           
            str = Regex.Replace(str, @"\s+", " ").Trim(); // convert multiple spaces into one space   
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim(); // cut and trim it   
            str = Regex.Replace(str, @"\s", "-"); // hyphens   

            return str;
        }
    }
}