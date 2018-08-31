using DeliveryService.Common.DTOs;
using DeliveryService.Common.Interfaces.DAL;
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

        private UserManager<IdentityUser> userManager;

        public AuthenticationRepository()
        {
            this.dbContext = new DeliveryServiceIdentityDbContext();
            this.userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(this.dbContext));
        }

        public IdentityResult RegisterUser(UserDTO userModel)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = userModel.Username
            };

            return this.userManager.Create(user, userModel.Password);
        }

        public Task<IdentityUser> FindUser(string userName, string password)
        {
            return await this.userManager.FindAsync(userName, password);
        }

        public void Dispose()
        {
            this.dbContext.Dispose();
            this.userManager.Dispose();

        }
    }
}
