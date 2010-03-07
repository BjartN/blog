using System;
using System.IO;
using System.Web;
using Autofac.Integration.Web;
using Blog.Infrastructure.MetaWeblogApi.Entities;
using CookComputing.XmlRpc;
using Elmah;

namespace Blog.Infrastructure.MetaWeblogApi
{
    [InjectProperties]
    public class MetaWeblogHandler : XmlRpcService, IRpcMetaWeblog
    {
        public IMetaWeblog Inner { private get; set; }

        public object editPost(string postid, string username, string password, Post post, bool publish)
        {
            return Inner.EditPost(postid, username, password, post, publish);
        }

        public CategoryInfo[] getCategories(string blogid, string username, string password)
        {
            return Inner.GetCategories(blogid, username, password);
        }

        public Post getPost(string postid, string username, string password)
        {
            return Inner.GetPost(postid, username, password);
        }

        public Post[] getRecentPosts(string blogid, string username, string password, int numberOfPosts)
        {
            return Inner.GetRecentPosts(blogid, username, password, numberOfPosts);
        }

        public string newPost(string blogid, string username, string password, Post post, bool publish)
        {
            return Inner.NewPost(blogid, username, password, post, publish);
        }

        public UrlData newMediaObject(string blogid, string username, string password, FileData file)
        {
            return Inner.NewMediaObject(blogid, username, password, file);
        }

        public BlogInfo[] getUsersBlogs(string appKey, string username, string password)
        {
            try
            {
                return Inner.GetUsersBlogs(appKey, username, password);
            }
            catch(Exception ex)
            {  
                ErrorSignal.FromCurrentContext().Raise(ex);
                throw;
            }
        }

        public bool deletePost(string appKey, string postid, string username, string password, bool publish)
        {
            return Inner.DeletePost(appKey, postid,username, password,publish);
        }
    }
}