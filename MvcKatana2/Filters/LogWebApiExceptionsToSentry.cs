using System.Web.Http.ExceptionHandling;
using SharpRaven;
using SharpRaven.Logging;

namespace MvcKatana2.Filters
{
    public class LogWebApiExceptionsToSentry : ExceptionLogger, IScrubber
    {
        private readonly IRavenClient _ravenClient;

        public LogWebApiExceptionsToSentry()
        {
            _ravenClient = new RavenClient("https://df069977eb1c400eb3c98711475aa566:40023092418b4442a652fe491f42780f@app.getsentry.com/19527");
            _ravenClient.Logger = "webApi";
            _ravenClient.LogScrubber = this;
        }

        public override void Log(ExceptionLoggerContext context)
        {
            _ravenClient.CaptureException(context.Exception);
        }
        
        public string Scrub(string input)
        {
            //TODO: scrub .AspNet.ApplicationCookie
            return input;
        }
    }
}
