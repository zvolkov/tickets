using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using MvcKatana2.Models;
using NPoco;
using Owin;

namespace MvcKatana2
{    
    public partial class OwinStartup
    {
        private static void ConfigureIoc(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            //MVC
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterModule(new AutofacWebTypesModule());
            //builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            //builder.RegisterModelBinderProvider();
            //builder.RegisterFilterProvider();

            //Web API
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            //builder.RegisterWebApiFilterProvider(GlobalConfiguration.Configuration);

            //SignalR
            builder.Register(c => GlobalHost.ConnectionManager).As<IConnectionManager>().ExternallyOwned();

            CustomRegistrations(builder);

            var container = builder.Build();
            
            //MVC
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            //WebApi
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static void CustomRegistrations(ContainerBuilder builder)
        {
            //plug custom App registrations here:
            builder.Register(c => new Database("local")).As<IDatabase>().InstancePerLifetimeScope();
        }
    }
}
