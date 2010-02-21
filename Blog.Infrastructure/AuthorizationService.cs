using System.Configuration;
using System.Web;

namespace Blog.Controllers
{
    public class AuthorizationService: IAuthorizationService
    {
        public bool IsCool()
        {
            return HttpContext.Current.User.Identity.IsAuthenticated;
        }
    }

    public interface IAuthorizationService
    {
        bool IsCool();
    }
}