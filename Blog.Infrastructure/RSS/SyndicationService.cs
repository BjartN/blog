using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel.Syndication;
using Blog.Core;

namespace Blog.Infrastructure.RSS
{
    /// <summary>
    /// Builds up a syndication feed based on appliction data
    /// </summary>
    public class SyndicationService : ISyndicationService
    {
        private readonly IRepository _respository;
        private readonly IUrlContext _urlContext;

        public SyndicationService(IRepository respository, IUrlContext urlContext)
        {
            _respository = respository;
            _urlContext = urlContext;
        }

        public SyndicationFeed CreateSyndicationFeed()
        {
            var posts = Post.GetPublishedPosts(_respository);
            var blog = _respository.List<BlogSettings>().Single();

            var myFeed = new SyndicationFeed
            {
                Title = new TextSyndicationContent(blog.Title),
                Description = new TextSyndicationContent(blog.Description),
                Language = CultureInfo.CurrentCulture.Name,
            };

            var feedItems = new List<SyndicationItem>();
            foreach(var p in posts)
            {
                var item = new SyndicationItem
                {
                    Title = new TextSyndicationContent(p.Title),
                    Summary = new TextSyndicationContent(p.Body),
                    PublishDate = new DateTimeOffset(DateTime.Now),
                    Id = p.Id,
                    BaseUri = new Uri(_urlContext.GetPostUrl(p))
                };

                var authInfo = new SyndicationPerson { Name = "Bjarte Djuvik Næss" };
                item.Authors.Add(authInfo);

                feedItems.Add(item);
            }
            myFeed.Items = feedItems;

            return myFeed;
        }
    }
}