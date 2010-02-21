using System.Web.Mvc;
using System.Xml;

namespace Blog.Infrastructure.RSS
{
    public class RssActionResult : ActionResult
    {
        private ISyndicationService _syndicationService;

        public RssActionResult(ISyndicationService syndicationService)
        {
            _syndicationService = syndicationService;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.ContentType = "text/xml";
            var myFeed = _syndicationService.CreateSyndicationFeed();
            var writer = XmlWriter.Create(context.HttpContext.Response.Output);
            myFeed.SaveAsRss20(writer);
            writer.Close();
        }
    }
}