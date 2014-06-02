using System.Web.Mvc;
using MvcKatana2.Filters;

namespace MvcKatana2
{
    public class MvcFilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //order is important, these run from the bottom up
            filters.Add(new LogMvcExceptionsToElmah());
            filters.Add(new HandleErrorAttribute());
            filters.Add(new LogMvcExceptionsToSentry());
            
            filters.Add(new AuthorizeAttribute());
        }
    }
}
