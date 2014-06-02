using System.Collections.Generic;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MvcKatana2.Models;

namespace MvcKatana2.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.Property1 = "blah-l!!";
            var t = new Ticket{id=1, description = "bbb"};
            return View(t);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string login, string password, string returnUrl)
        {
            Request.GetOwinContext().Authentication.SignOut();
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, login),
                new Claim(ClaimTypes.Name, login)
            };

            var id = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
            Request.GetOwinContext().Authentication.SignIn(id);
            return Redirect(returnUrl ?? "/");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut();
            return Redirect("/");
        }
	}
}
