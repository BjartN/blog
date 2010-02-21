using System;
using Blog.Infrastructure.MetaWeblogApi.Entities;
using NUnit.Framework;
using System.IO;

namespace Blog.Specs.MetaWeblog
{
    [TestFixture]
    public class when_adding_media : MetaWeblogContext
    {
        [Test]
        public void should_store_it()
        {
            var media = new FileData
            {
                bits = File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "rssicon.png")),
                name= "rssicon_target.png",
                type = "image"
            };

            var res = _api.NewMediaObject("1000", "BjartN", "", media);
            Assert.That(File.Exists(Path.Combine(Directory.GetCurrentDirectory(),media.name)), "Media not persisted");
        }
    }

    [TestFixture]
    public class when_getting_recent_posts : MetaWeblogContext
    {
        [Test]
        public void should_get_posts()
        {
            var res = _api.GetRecentPosts("1000", "BjartN", "", 50);
            Assert.AreEqual(1,res.Length);
        }

        [Test]
        public void categories_should_not_be_null()
        {
            var res = _api.GetRecentPosts("1000", "BjartN", "", 50);
            Assert.IsNotNull(res[0].categories);
        }
    }

    [TestFixture]
    public class when_adding_new_post : MetaWeblogContext
    {
        [Test]
        public void should_be_able_to_extract_it()
        {
            var inputPost = new Post
            {
                userid = "BjartN",
                dateCreated = DateTime.Now,
                title = "Hello world",
                description = "Text",
                categories = new string[0]
            };

            var newId = _api.NewPost("1000", "BjartN", "", inputPost, true);
            var newPost = _api.GetPost(newId, "BjartN", "");

            Assert.AreEqual(inputPost.userid, newPost.userid);
            Assert.AreEqual(inputPost.title, newPost.title);
            Assert.AreEqual(inputPost.description, newPost.description);
        }
    }
}