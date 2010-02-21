using Blog.Infrastructure.MetaWeblogApi.Entities;

namespace Blog.Infrastructure.MetaWeblogApi
{
    public interface IMetaWeblog
    {
        object EditPost(
            string postId,
            string userName,
            string passWord,
            Post post,
            bool publish);

        CategoryInfo[] GetCategories(
            string blogId,
            string userName,
            string password);

        Post GetPost(
            string postId,
            string userName,
            string password);

        Post[] GetRecentPosts(
            string blogId,
            string userName,
            string password,
            int numberOfPosts);

        string NewPost(
            string blogId,
            string userName,
            string password,
            Post post,
            bool publish);

        UrlData NewMediaObject(
            string blogId,
            string userName,
            string password,
            FileData file);

        BlogInfo[] GetUsersBlogs(
            string appKey,
            string userName,
            string password);

        bool DeletePost(
            string appKey,
            string postId,
            string userName,
            string password,
            bool publish);
    }
}