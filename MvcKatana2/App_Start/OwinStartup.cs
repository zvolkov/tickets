using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MvcKatana2.OwinStartup))]

namespace MvcKatana2
{    
    public partial class OwinStartup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            app.MapSignalR(
                new HubConfiguration
                {
#if DEBUG
                    EnableDetailedErrors = true
#endif
                }
            );

            ConfigureIoc(app);
        }
    }
}
