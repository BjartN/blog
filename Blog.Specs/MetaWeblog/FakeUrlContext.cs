using System.IO;
using Blog.Core;
using Blog.Infrastructure;
using Post=Blog.Core.Post;

namespace Blog.Specs
{
    public class FakeUrlContext : IUrlContext
    {
        public string AbsoluteUrlWithDomain(string relative)
        {
            return "";
        }

        public string MapPath(string virtualUrl)
        {
            return Directory.GetCurrentDirectory();
        }

        public string GetPostUrl(Post p)
        {
            return "";
        }
    }

    public class FakeAuthenticationService:IAuthenticationService
    {
        public bool Authenticate(string username, string password, bool remember)
        {
            return true;
        }
    }
}