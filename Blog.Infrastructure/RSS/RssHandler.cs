using System.Web;
using System.Xml;
using Autofac.Integration.Web;

namespace Blog.Infrastructure.RSS
{
    [InjectProperties]
    public class RssHandler : IHttpHandler
    {
        public ISyndicationService SyndicationService { get; set; }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/xml";

            var myFeed = SyndicationService.CreateSyndicationFeed();

            var writer = XmlWriter.Create(context.Response.Output);
            myFeed.SaveAsRss20(writer);
            writer.Close();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

}
