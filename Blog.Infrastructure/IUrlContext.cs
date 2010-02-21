namespace Blog.Infrastructure
{
    public interface IUrlContext
    {
        string AbsoluteUrlWithDomain(string relative);
        string MapPath(string virtualUrl);
        string GetPostUrl(Core.Post p);
    }
}