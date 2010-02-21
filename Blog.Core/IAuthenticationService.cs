namespace Blog.Core
{
    public interface IAuthenticationService
    {
        bool Authenticate(string username, string password,bool remember);
    }
}