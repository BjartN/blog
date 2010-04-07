using System.Web.Routing;
using Blog.Web;
using NUnit.Framework;
using TumblrImport;


namespace Blog.Specs
{
    [TestFixture]
    public class RoutesTest
    {
        private RouteCollection _routes;

        [SetUp]
        public void setup()
        {
            _routes = new RouteCollection();
            Global.RegisterRoutes(_routes);
        }

        [Test]
        public void should_map_path_rss_to_rss_controller()
        {
            RouteHelpers.AssertRoute(_routes, "~/rss", new { controller = "Rss", action = "Index" });
        }

        [Test]
        public void should_map_path_PostIdSlug_to_post_controller()
        {
            RouteHelpers.AssertRoute(_routes, "~/post/12345/my-slug", new { controller = "Post", action = "Index", id="12345", slug="my-slug" });
        }

        [Test]
        public void should_map_PostSlug_to_post_controller()
        {
            RouteHelpers.AssertRoute(_routes, "~/post/my-slug", new { controller = "Post", action = "GetPostBySlug", slug = "my-slug" });
        }

    }
}