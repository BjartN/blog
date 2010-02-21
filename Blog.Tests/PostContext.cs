using System.Collections.Generic;
using System.IO;
using Blog.Core;
using Blog.Infrastructure.Db4o;
using NUnit.Framework;

namespace Blog.Tests
{
    public class PostContext
    {
        protected IRepository _repository;
        protected IList<Post> _savedPosts;

        [SetUp]
        public virtual void setup()
        {
            _savedPosts = new List<Post>();

            var file = Path.Combine(Directory.GetCurrentDirectory(), "db.yap");
            if (File.Exists(file))
                File.Delete(file);

            _repository = new Repository(file);

            for (var i = 0; i < 10; i++)
            {
                var post = Post.CreatePost("Title" + i, "Body" + i, "BjartN", new List<Tag> {new Tag("tag"  + i), new Tag("tag" + (i+1))});
                _repository.Save(post);
                _savedPosts.Add(post);
            }
        }

        [TearDown]
        public void teardown()
        {
            ((Repository)_repository).Dispose();
        }
    }
}