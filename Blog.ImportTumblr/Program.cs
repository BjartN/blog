using System.Linq;
using Blog.Core;
using Blog.Infrastructure.Db4o;
using Blog.Infrastructure.Tumblr;
using Post=Blog.Core.Post;

namespace Blog.ImportTumblr
{
    class Program
    {
        static void Main(string[] args)
        {
            var _repository = new Repository(@"C:\Users\BjartN\Documents\Visual Studio 2008\Projects\Blog\Blog\App_Data\Db4o.yap");
            var _oldBlogs =
                new Importer(@"C:\Users\BjartN\Documents\Visual Studio 2008\Projects\Blog\Blog.Tests\read.xml");
             var b = new BlogSettings
            {
                Title = "Bjarte.Com",
                Description = "Software architecture,design, process and business",
                VirtualMediaPath = "~/Uploads"
            };
            _repository.Save(b);
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
        }
    }
}
