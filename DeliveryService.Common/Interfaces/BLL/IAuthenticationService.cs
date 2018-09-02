using DeliveryService.Common.DTOs;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Common.Interfaces.BLL
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// Registers a new admin user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        IdentityResult RegisterAdminUser(UserDTO user);

        /// <summary>
        /// Searches for a user.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>The user identity.</returns>
        IdentityUser FindUser(string username, string password);

        /// <summary>
        /// Returns list of role names from a specific user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        ICollection<string> GetUserRoles(string userId);
    }
}
