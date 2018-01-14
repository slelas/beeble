using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Beeble.Api.Providers;
using Beeble.Api.UserRoles;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Beeble.Api.Scheduler;

[assembly: OwinStartup(typeof(Beeble.Api.Startup))]
namespace Beeble.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureOAuth(app);

            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config); //cors inace ne radi
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            CreateRoles.Execute();
            Seed.Execute();

            // One minute = 60 000 milliseconds
            //SchedulerService.StartAction(60000, Actions.DeleteExpiredRegistrations);
        }

    }
}