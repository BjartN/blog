using System.Web;
using System.Web.Mvc;
using Blog.Core;
using Blog.Web.Controllers;
using Microsoft.Web.Mvc;

namespace Blog.Web.Code
{
    public static class HtmlHelperExtentions
    {
        //Texas in here...

        public static string PostUrl(this HtmlHelper helper, Post p)
        {
            if (!string.IsNullOrEmpty(p.LegacyUrl))
            {
                var url = p.LegacyUrl.ToLower();

                if (url.Contains("http://"))
                {
                    url = url.Replace("http://", "");
                    if (url.IndexOf("/") > 0)
                        url = url.Substring(url.IndexOf("/"));
                }

                return url;
            }

            return helper.BuildUrlFromExpression<PostController>(x => x.GetPostBySlug(p.Slug));
        }

        public static string FullPostUrl(this HtmlHelper helper, Post p)
        {
            return getHost() + PostUrl(helper, p);
        }

        public static string FacebookUrl(this HtmlHelper helper, Post p)
        {
            return string.Format("http://www.facebook.com/sharer.php?u={0}&t={1}",
                urlEncode(FullPostUrl(helper, p)),
                urlEncode(p.Title));
        }

        public static string TwitterUrl(this HtmlHelper helper, Post p)
        {
            var message = "\"" + p.Title + "\" " + helper.FullPostUrl(p);
            return string.Format("http://twitter.com/home?status={0}", urlEncode(message));
        }

        private static string getHost()
        {
            return string.Format("http://{0}", HttpContext.Current.Request.Url.Host);
        }

        private static string urlEncode(string urlContent)
        {
            return HttpContext.Current.Server.UrlEncode(urlContent);
        }
    }
}