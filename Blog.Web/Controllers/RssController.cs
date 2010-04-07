using System.Web.Mvc;
using Blog.Infrastructure.RSS;

namespace Blog.Web.Controllers
{
    public class RssController : Controller
    {
        private readonly ISyndicationService _syndication;

        public RssController(ISyndicationService result)
        {
            _syndication = result;
        }

        public ActionResult Index()
        {
            return new RssActionResult(_syndication);
        }

    }
}
