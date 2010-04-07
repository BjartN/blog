using System.Web.Mvc;
using Blog.Core;
using Blog.Models;
using System.Linq;
using Post = Blog.Core.Post;

namespace Blog.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        private readonly IRepository _repository;

        public HomeController(IRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            return View(new HomeModel {Posts = Post.GetPublishedPosts(_repository).Take(10), Settings = BlogSettings.Get(_repository)});
        }
    }
}
