using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeliveryService.Identity.DAL.Entities
{
    public class DeliveryServiceUserManager : UserManager<DeliveryServiceUser>
    {
        public DeliveryServiceUserManager(IUserStore<DeliveryServiceUser> store)
        : base(store)
        {
        }

        // this method is called by Owin therefore best place to configure your User Manager
        public static DeliveryServiceUserManager Create(
            IdentityFactoryOptions<DeliveryServiceUserManager> options, IOwinContext context)
        {
            var manager = new DeliveryServiceUserManager(
                new UserStore<DeliveryServiceUser>(context.Get<DeliveryServiceIdentityDbContext>()));

            // optionally configure your manager
            // ...

            return manager;
        }
    }
}