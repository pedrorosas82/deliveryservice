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
        public AuthenticationRepository()
        {

        }

        public IdentityResult RegisterAdminUser(UserDTO userModel)
        {
            IdentityResult result;

            DeliveryServiceUser userEntity = new DeliveryServiceUser
            {
                UserName = userModel.Username
            };

            using (var dbContext = new DeliveryServiceIdentityDbContext())
            {
                // create user
                var userManager = new DeliveryServiceUserManager(new UserStore<DeliveryServiceUser>(dbContext));
                result = userManager.Create(userEntity, userModel.Password);

                // add to role
                var roleManager = new RoleManager<DeliveryServiceRole>(new RoleStore<DeliveryServiceRole>(dbContext));
                DeliveryServiceRole adminRole = roleManager.FindByName("Administrator");

                if (adminRole == null)
                {
                    roleManager.Create(new DeliveryServiceRole { Name = "Administrator" });
                }

                userManager.AddToRole(userEntity.Id, "Administrator");
            }

            return result;
        }

        public IdentityUser FindUser(string username, string password)
        {
            IdentityUser result;

            using (var dbContext = new DeliveryServiceIdentityDbContext())
            {
                var userManager = new DeliveryServiceUserManager(new UserStore<DeliveryServiceUser>(dbContext));
                result = userManager.Find(username, password);
            }

            return result;
        }
    }
}
