using DeliveryService.Common;
using DeliveryService.Common.DTOs;
using DeliveryService.Common.Interfaces.BLL;
using DeliveryService.Common.Interfaces.DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.BLL
{
    /// <summary>
    /// This class provides a service that encapsulates the business logic for the Identity Provider.
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        private IAuthenticationRepository authenticationRepository;

        public AuthenticationService(IAuthenticationRepository authenticationRepository)
        {
            this.authenticationRepository = authenticationRepository;
        }

        /// <summary>
        /// Searches for a user with the provided credentials.
        /// </summary>
        /// <param name="username">The username</param>
        /// <param name="password">The password</param>
        /// <returns>An identity of the user. If no match is found, returns null.</returns>
        public IdentityUser FindUser(string username, string password)
        {
            return this.authenticationRepository.FindUser(username, password);
        }

        /// <summary>
        /// Returns the list of all roles assigned to a user.
        /// </summary>
        /// <param name="userId">The user Id.</param>
        /// <returns>The list of role names the user is enrolled.</returns>
        public ICollection<string> GetUserRoles(string userId)
        {
            return this.authenticationRepository.GetUserRoles(userId);
        }

        /// <summary>
        /// Creates a new user with the "Administrator" role.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IdentityResult RegisterAdminUser(UserDTO user)
        {
            return this.authenticationRepository.RegisterAdminUser(user);
        }
    }
}
