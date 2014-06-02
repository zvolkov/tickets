using System;
using System.Security.Claims;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MvcKatana2
{
    public class Global : HttpApplication
    {
        public static string Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

        // Code that runs on application startup
        void Application_Start(object sender, EventArgs e)
        {
            //sequence is important. Web API routes must go first, otherwise they will not be found
            GlobalConfiguration.Configure(WebApiConfig.Register);

            MvcFilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            MvcRouteConfig.RegisterRoutes(RouteTable.Routes);

            //http://brockallen.com/2012/07/08/mvc-4-antiforgerytoken-and-claims/
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;

            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
