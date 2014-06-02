using System.Web.Mvc;
using SharpRaven;
using SharpRaven.Logging;

namespace MvcKatana2.Filters
{
    public class LogMvcExceptionsToSentry : IExceptionFilter, IScrubber
    {
        private readonly IRavenClient _ravenClient;

        public LogMvcExceptionsToSentry()
        {
            _ravenClient = new RavenClient("https://df069977eb1c400eb3c98711475aa566:40023092418b4442a652fe491f42780f@app.getsentry.com/19527");
            _ravenClient.Logger = "mvc";
            _ravenClient.LogScrubber = this;
        }

        public void OnException(ExceptionContext filterContext)
        {
            _ravenClient.CaptureException(filterContext.Exception);
        }

        public string Scrub(string input)
        {
            //TODO: scrub .AspNet.ApplicationCookie
            return input;
        }
    }
}
