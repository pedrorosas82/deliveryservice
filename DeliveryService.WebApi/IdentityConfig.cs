using DeliveryService.Common.Interfaces.BLL;
using DeliveryService.DAL;
using DeliveryService.Identity.DAL;
using DeliveryService.Identity.DAL.Entities;
using DeliveryService.WebApi.Providers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Data.Entity;
using System.Web.Http;
using Unity;

namespace DeliveryService.WebApi
{
    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureOAuth(app);

            Database.SetInitializer<DeliveryServiceDbContext>(new CreateDatabaseIfNotExists<DeliveryServiceDbContext>());
            Database.SetInitializer<DeliveryServiceIdentityDbContext>(new CreateDatabaseIfNotExists<DeliveryServiceIdentityDbContext>());
            
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        private void ConfigureOAuth(IAppBuilder app)
        {
            IUnityContainer container = UnityConfig.Container;

            Func<IAuthenticationService> authenticationServiceFactory = () =>
              container.Resolve<IAuthenticationService>();

            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/v1/auth/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider(authenticationServiceFactory)
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
