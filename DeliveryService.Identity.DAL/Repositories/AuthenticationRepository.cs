using DeliveryService.Common.DTOs;
using DeliveryService.Common.Interfaces.DAL;
using DeliveryService.Identity.DAL.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Identity.DAL.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private DeliveryServiceIdentityDbContext dbContext;

        private DeliveryServiceUserManager userManager;

        public AuthenticationRepository()
        {
            this.dbContext = new DeliveryServiceIdentityDbContext();
            this.userManager = new DeliveryServiceUserManager(new UserStore<DeliveryServiceUser>(this.dbContext));
        }

        public IdentityResult RegisterUser(UserDTO userModel)
        {
            DeliveryServiceUser user = new DeliveryServiceUser
            {
                UserName = userModel.Username
            };

            return this.userManager.Create(user, userModel.Password);
        }

        public IdentityUser FindUser(string username, string password)
        {
            return this.userManager.FindAsync(username, password).Result;
        }

        public void Dispose()
        {
            this.dbContext.Dispose();
            this.userManager.Dispose();
        }
    }
}
