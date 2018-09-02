﻿using DeliveryService.Common.DTOs;
using DeliveryService.Common.Interfaces.BLL;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace DeliveryService.WebApi.Controllers
{
    public class AccountsController : ApiController
    {
        private IAuthenticationService authenticationService;

        public AccountsController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [Route("RegisterAdmin")]
        public IHttpActionResult RegisterAdmin(UserDTO userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = this.authenticationService.RegisterAdminUser(userModel);
            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            return Ok();
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}