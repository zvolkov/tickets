using System;
using System.Web;
using System.Web.Mvc;
using Elmah;

namespace MvcKatana2.Filters
{
    public class LogMvcExceptionsToElmah : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            // Log only handled exceptions, because all other will be caught by ELMAH anyway.
            if (context.ExceptionHandled)
            {
                // Wrap the exception in an HttpUnhandledException so that ELMAH can capture the original error page.
                Exception exceptionToRaise = new HttpUnhandledException(message: null, innerException: context.Exception);

                // Send the exception to ELMAH (for logging, mailing, filtering, etc.).
                ErrorSignal.FromCurrentContext().Raise(exceptionToRaise);

            }
        }
    }
}
