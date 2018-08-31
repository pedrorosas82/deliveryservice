using DeliveryService.Common.Interfaces.BLL;
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

namespace DeliveryService.WebApi
{
    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app, IAuthenticationService authenticationService)
        {
            ConfigureOAuth(app, authenticationService);
        }

        private void ConfigureOAuth(IAppBuilder app, IAuthenticationService authenticationService)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider(authenticationService)
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }
    }
}
