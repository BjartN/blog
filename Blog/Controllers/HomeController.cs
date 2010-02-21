using MvcContrib;
using System.Web.Mvc;
using Blog.Core;
using Blog.Infrastructure.Tumblr;
using Blog.Models;
using System.Linq;
using Post = Blog.Core.Post;

namespace Blog.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        private readonly IRepository _repository;
        private readonly Importer _oldBlogs;
        private readonly IAuthorizationService _authorizationService;

        public HomeController(IRepository repository, Importer oldBlogs, IAuthorizationService authorizationService)
        {
            _repository = repository;
            _oldBlogs = oldBlogs;
            _authorizationService = authorizationService;
        }

        public ActionResult Reload()
        {
            //if (!_authorizationService.IsCool())
            //    return this.RedirectToAction(x => x.Index());

            var b = new BlogSettings
            {
                Title = "Bjarte.Com",
                Description = "Software architecture,design, process and business",
                VirtualMediaPath = "~/Uploads"
            };

            foreach (var p in _oldBlogs.Import())
            {
                var post = Post.CreateLegacyPost(
                    p.Title,
                    p.Body, 
                    "BjarN",
                    p.Tags.Select(x=>new Tag(x)).ToList(),
                    p.GmtDate,
                    p.Url,
                    p.Id
                );

                _repository.Save(post);
            }
            _repository.Save(b);

            return this.RedirectToAction(x => x.Index());
        }

        public ActionResult Index()
        {
            return View(new HomeModel {Posts = Post.GetPublishedPosts(_repository).Take(10), Settings = BlogSettings.Get(_repository)});
        }
    }
}
