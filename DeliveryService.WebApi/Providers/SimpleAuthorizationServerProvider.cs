using DeliveryService.Common.Interfaces.BLL;
using DeliveryService.Identity.DAL.Entities;
using DeliveryService.Identity.DAL.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace DeliveryService.WebApi.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private Func<IAuthenticationService> authenticationServiceFactory;

        private IAuthenticationService authenticationService
        {
            get
            {
                return this.authenticationServiceFactory.Invoke();
            }
        }

        public SimpleAuthorizationServerProvider(Func<IAuthenticationService> authenticationServiceFactory)
        {
            this.authenticationServiceFactory = authenticationServiceFactory;
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            await Task.Run(() => {
                context.Validated();
            });
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            await Task.Run(() => {
                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

                IdentityUser user = this.authenticationService.FindUser(context.UserName, context.Password);

                if (user == null)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }

                // add claims
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, context.UserName));

                foreach (IdentityUserRole role in user.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
                }

                var claimsIdentity = new ClaimsIdentity(claims, context.Options.AuthenticationType);

                context.Validated(claimsIdentity);
            });
        }
    }
}