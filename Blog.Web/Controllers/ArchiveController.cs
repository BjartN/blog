using System.Web.Mvc;
using Blog.Core;
using Blog.Models;
using System.Linq;
using Post = Blog.Core.Post;

namespace Blog.Controllers
{
    [HandleError]
    public class ArchiveController : Controller
    {
        private readonly IRepository _repository;

        public ArchiveController(IRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            var posts = Post.GetArchive(_repository);
            return View(new ArchiveModel{Posts=posts.ToList()});
        }
    }
}
