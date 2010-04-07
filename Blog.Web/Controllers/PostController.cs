using System.Linq;
using System.Web.Mvc;
using Blog.Controllers;
using Blog.Core;
using Blog.Models;
using MvcContrib;

namespace Blog.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly IRepository _repository;

        public PostController(IRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Legacy action for legacy urls
        /// </summary>
        public ActionResult Index(string id, string slug)
        {
            var post = Post.GetPostForDisplay(_repository, id);
            
            if(post==null)
                return this.RedirectToAction<HomeController>(x => x.Index());
                
            if(!string.IsNullOrEmpty(slug) && post.Slug!=slug)
                return this.RedirectToAction<HomeController>(x => x.Index());
                
            return View(new PostModel {Post = post});
        }

        public ActionResult GetAllPosts()
        {
            return View(new HomeModel { Posts = Post.GetPublishedPosts(_repository).Take(10), Settings = BlogSettings.Get(_repository) });
        }

        public ActionResult GetPostBySlug(string slug)
        {
            var post = Post.GetPostsBySlug(slug,_repository);

            if (post == null)
                return this.RedirectToAction<HomeController>(x => x.Index());

            if (!string.IsNullOrEmpty(slug) && post.Slug != slug)
                return this.RedirectToAction<HomeController>(x => x.Index());

            return View("Index", new PostModel { Post = post });
        }

        public ActionResult Tags()
        {
            return View(Post.GetTags(_repository).ToList());
        }

        public ActionResult PostsByTag(string tag)
        {
            var posts = Post.GetPostsByTag(tag, _repository);
            return View("~/Views/Home/Index.aspx", new HomeModel { Posts = posts, Settings = BlogSettings.Get(_repository) });
        }

    }
}
