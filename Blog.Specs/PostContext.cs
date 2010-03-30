using System.Collections.Generic;
using Blog.Core;
using Blog.Infrastructure.MongoDb;
using NUnit.Framework;
using System;

namespace Blog.Specs
{
    public class PostContext
    {
        protected IRepository _repository;
        protected IList<Post> _savedPosts;

        [SetUp]
        public virtual void setup()
        {
            _savedPosts = new List<Post>();

            var mongoRepository = new MongoRepository("blogspecs");
            mongoRepository.DeleteCollection<BlogSettings>();
            mongoRepository.DeleteCollection<Post>();

            _repository = mongoRepository;

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
            if(_repository is IDisposable)
                ((IDisposable)_repository).Dispose();
        }
    }
}