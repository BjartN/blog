using System.ServiceModel.Syndication;

namespace Blog.Infrastructure.RSS
{
    public interface ISyndicationService
    {
        SyndicationFeed CreateSyndicationFeed();
    }
}