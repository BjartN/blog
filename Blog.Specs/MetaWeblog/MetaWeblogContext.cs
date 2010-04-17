using Blog.Core;
using NUnit.Framework;
using Blog.Infrastructure.MongoDb;

namespace Blog.Specs
{
    public abstract class MetaWeblogContext
    {
        protected IRepository _repository;
        protected Infrastructure.MetaWeblogApi.MetaWeblog _api;
        protected BlogSettings _blog;
        protected Post _fakePost;

        [SetUp]
        public virtual void setup()
        {
            var mongoRepository = new MongoRepository("blogspecs");
            mongoRepository.DeleteCollection<BlogSettings>();
            mongoRepository.DeleteCollection<Post>();

            _repository = mongoRepository;


            _blog = new BlogSettings
            {
                VirtualMediaPath = ""
            };
            _repository.Save(_blog);

            _fakePost = Post.CreatePost("Hello world", "", "BjartN", null);

            _repository.Save(_fakePost);

            _api = new  Infrastructure.MetaWeblogApi.MetaWeblog(_repository, new FakeUrlContext(),new FakeAuthenticationService());
        }



        [TearDown]
        public void teardown()
        {
            ((MongoRepository)_repository).Dispose();
        }
    }

    
}