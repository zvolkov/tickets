using System;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace MvcKatana2
{    
    public partial class OwinStartup
    {
        private static void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                ReturnUrlParameter = "returnUrl",
                AuthenticationMode = AuthenticationMode.Active,
                ExpireTimeSpan = TimeSpan.FromHours(2),
                //http://brockallen.com/2013/10/27/using-cookie-authentication-middleware-with-web-api-and-401-response-codes/
                Provider = new CookieAuthenticationProvider { OnApplyRedirect = RedirectUnlessAjax }
            });
        }
        
        private static void RedirectUnlessAjax(CookieApplyRedirectContext ctx)
        {
            if (!IsAjaxRequest(ctx.Request))
            {
                ctx.Response.Redirect(ctx.RedirectUri);
            }
        }

        private static bool IsAjaxRequest(IOwinRequest request)
        {
            var query = request.Query;
            if ((query != null) && (query["X-Requested-With"] == "XMLHttpRequest"))
            {
                return true;
            }
            var headers = request.Headers;
            return ((headers != null) && (headers["X-Requested-With"] == "XMLHttpRequest"));
        }
    }
}
