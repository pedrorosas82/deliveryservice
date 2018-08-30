using DeliveryService.Identity.DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace DeliveryService.Identity.DAL
{
    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(() => new DeliveryServiceIdentityDbContext());
            app.CreatePerOwinContext<DeliveryServiceUserManager>(DeliveryServiceUserManager.Create);
            app.CreatePerOwinContext<RoleManager<DeliveryServiceRole>>((options, context) =>
                new RoleManager<DeliveryServiceRole>(
                    new RoleStore<DeliveryServiceRole>(context.Get<DeliveryServiceIdentityDbContext>())));

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Home/Login"),
            });
        }
    }
}
