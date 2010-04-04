using System.Web;
using System.Web.Mvc;
using Blog.Core;
using Blog.Infrastructure;

namespace Blog.Web.Code
{
    public static class HtmlHelperExtentions
    {
        public static string PostUrl(this HtmlHelper helper, Post p)
        {
            return UrlContext.Instance.GetPostUrl(p);
        }
        
        public static string FacebookUrl(this HtmlHelper helper, Post p)
        {
            return string.Format("http://www.facebook.com/sharer.php?u={0}&t={1}",
                urlEncode(PostUrl(helper, p)),
                urlEncode(p.Title));
        }

        public static string TwitterUrl(this HtmlHelper helper, Post p)
        {
            var message = "\"" + p.Title + "\" " + helper.PostUrl(p);
            return string.Format("http://twitter.com/home?status={0}", urlEncode(message));
        }
 
        private static string urlEncode(string urlContent)
        {
            return HttpContext.Current.Server.UrlEncode(urlContent);
        }
    }
}