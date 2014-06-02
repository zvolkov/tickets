using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using MvcKatana2.Filters;

namespace MvcKatana2
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            //http://www.asp.net/web-api/overview/web-api-routing-and-actions/routing-in-aspnet-web-api
            config.Routes.MapHttpRoute(
                name: "ActionApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Filters.Add(new AuthorizeAttribute());

            config.Services.Add(typeof(IExceptionLogger), new LogWebApiExceptionsToSentry());
            config.Services.Add(typeof(IExceptionLogger), new LogWebApiExceptionsToElmah());
        }
    }
}
