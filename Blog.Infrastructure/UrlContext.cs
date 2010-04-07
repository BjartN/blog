using System.Web;
using Blog.Core;

namespace Blog.Infrastructure
{
    public class UrlContext : IUrlContext
    {
        public string AbsoluteUrlWithDomain(string virutalUrl)
        {
            var absoluteUrl = VirtualPathUtility.ToAbsolute(virutalUrl);
            var url = HttpContext.Current.Request.Url;
            return string.Format("http://{0}:{1}{2}", url.Host, url.Port, absoluteUrl);
        }

        public string MapPath(string virtualUrl)
        {
            return HttpContext.Current.Server.MapPath(virtualUrl);
        }

        public string GetPostUrl(Post p)
        {
            if (!string.IsNullOrEmpty(p.LegacyUrl))
                return p.LegacyUrl.ToLower();

            return AbsoluteUrlWithDomain("~/post/" + p.Slug);
        }

        public static IUrlContext _instance = new UrlContext();
        public static IUrlContext Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}