using System.Collections.Generic;
using System.Linq;
using Blog.Core;
using Blog.Infrastructure;
using Moq;
using NUnit.Framework;
using Blog.Infrastructure.RSS;

namespace Blog.Tests
{
    [TestFixture]
    public class SyndicationFeedTests
    {
        private Mock<IRepository> _repository;
        private Mock<IUrlContext> _urlContext;
        private Post _post;

        [SetUp]
        public void setup()
        {
            _repository = new Mock<IRepository>();
            _urlContext = new Mock<IUrlContext>();

            _post = Post.CreatePost("test", "test", "test", null);

            _urlContext
                .Setup(x => x.GetPostUrl(_post))
                .Returns("http://bjarte.com");

            _repository
                .Setup(x => x.List<BlogSettings>())
                .Returns((new List<BlogSettings> { new BlogSettings() }).AsQueryable);

            _repository
                .Setup(x => x.List<Post>())
                .Returns((new List<Post> { _post }).AsQueryable);

        }

        [Test]
        public void should_publish_post_id()
        {
            var syndicationService = new SyndicationService(_repository.Object, _urlContext.Object);
            var syndicationItem = syndicationService.CreateSyndicationFeed().Items.First();
            Assert.AreEqual(_post.Id, syndicationItem.Id);
        }

        [Test]
        public void should_publish_post_url()
        {
            var syndicationService = new SyndicationService(_repository.Object, _urlContext.Object);
            var syndicationItem = syndicationService.CreateSyndicationFeed().Items.First();
            Assert.AreEqual("http://bjarte.com/", syndicationItem.BaseUri.ToString());
        }
    }
}