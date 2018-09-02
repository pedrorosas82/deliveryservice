using DeliveryService.Common.DTOs;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Common.Interfaces.DAL
{
    public interface IAuthenticationRepository
    {
        IdentityResult RegisterAdminUser(UserDTO user);
        IdentityUser FindUser(string username, string password);
        ICollection<string> GetUserRoles(string userId);
    }
}
