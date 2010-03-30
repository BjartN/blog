using System.Linq;
using Blog.Core;
using NUnit.Framework;

namespace Blog.Specs
{
    [TestFixture]
    public class PostTests:PostContext
    {
        [Test]
        public void should_get_all_published_posts()
        {
            var query = Post.GetPublishedPosts(_repository);
            Assert.AreEqual(10,query.Count());
        }

        [Test]
        public void should_get_posts_by_tag()
        {
            var query = Post.GetPostsByTag("tag3",_repository);
            Assert.AreEqual(2,query.Count());
        }

        [Test]
        public void slug_should_be_created_from_title()
        {
            var query = Post.GetPostsBySlug("Title1", _repository);
            Assert.AreEqual(query.Title,"Title1");
        }

        [Test]
        public void should_get_post()
        {
            var post = Post.GetPost(_savedPosts[0].Id, _repository);
            Assert.IsNotNull(post);
        }

        [Test]
        public void should_get_tags()
        {
            var query = Post.GetTags(_repository);
            Assert.AreEqual(11,query.Count());
        }

        [Test]
        public void should_delete_post()
        {
            var postToDelete = _savedPosts[0].Id;

            Post.Delete(postToDelete, _repository);
            var post  = Post.GetPost(postToDelete,_repository);

            Assert.AreEqual(null,post);
        }
    }
}