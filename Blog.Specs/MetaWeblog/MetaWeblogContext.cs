using System;
using System.IO;
using Blog.Core;
using Blog.Infrastructure.Db4o;
using NUnit.Framework;

namespace Blog.Specs
{
    public abstract class MetaWeblogContext
    {
        protected IRepository _repository;
        protected Infrastructure.MetaWeblogApi.MetaWeblog _api;
        protected BlogSettings _blog;

        [SetUp]
        public virtual void setup()
        {
            var file = Path.Combine(Directory.GetCurrentDirectory(), "db.yap");
            if (File.Exists(file))
                File.Delete(file);

            _repository = new Repository(file);
            _blog = new BlogSettings
            {
                VirtualMediaPath = ""
            };
            _repository.Save(_blog);

            var post = Post.CreatePost("Hello world", "", "BjartN", null);

            _repository.Save(post);

            _api = new  Infrastructure.MetaWeblogApi.MetaWeblog(_repository, new FakeUrlContext(),new FakeAuthenticationService());
        }



        [TearDown]
        public void teardown()
        {
            ((Repository)_repository).Dispose();
        }
    }

    
}