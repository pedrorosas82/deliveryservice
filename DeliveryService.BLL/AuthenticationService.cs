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
    public class AuthenticationService : IAuthenticationService
    {
        private IAuthenticationRepository authenticationRepository;

        public AuthenticationService(IAuthenticationRepository authenticationRepository)
        {
            this.authenticationRepository = authenticationRepository;
        }

        public IdentityUser FindUser(string username, string password)
        {
            return this.authenticationRepository.FindUser(username, password);
        }

        public IdentityResult RegisterUser(UserDTO user)
        {
            return this.authenticationRepository.RegisterUser(user);
        }
    }
}
