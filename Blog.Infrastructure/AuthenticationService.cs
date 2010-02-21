using System.Configuration;
using System.Web.Security;
using Blog.Core;

namespace Blog.Controllers
{
    public class AuthenticationService:IAuthenticationService
    {
        public bool Authenticate(string userName, string password, bool remember)
        {
            var requiredUserName = ConfigurationManager.AppSettings["username"];
            var requiredPassword = ConfigurationManager.AppSettings["password"];

            if (requiredPassword == password && requiredUserName == userName)
            {
                FormsAuthentication.SetAuthCookie(userName, remember);
                return true;
            }
            return false;
        }
    }
}