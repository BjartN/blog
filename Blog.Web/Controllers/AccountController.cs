using System;
using System.Web.Mvc;
using System.Web.Security;
using Blog.Core;
using Blog.Models;
using MvcContrib;

namespace Blog.Controllers
{

    [HandleError]
    public class AccountController : Controller
    {
        private IAuthenticationService _authenticationService;

        public AccountController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return this.RedirectToAction<HomeController>(x => x.Index());
        }

        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if(_authenticationService.Authenticate(model.UserName,model.Password, model.RememberMe))
                    return this.RedirectToAction<HomeController>(x=>x.Index());
            }

            return View(model);
        }

    }
}
