using DeliveryService.Common.DTOs;
using DeliveryService.Common.Interfaces.BLL;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace DeliveryService.WebApi.Controllers
{
    [RoutePrefix("api/v1/accounts")]
    public class AccountsController : ApiController
    {
        private IAuthenticationService authenticationService;

        public AccountsController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        /// <summary>
        /// This action is just for demonstration purposes and would never be available this way in production.
        /// It creates a new admin user in the Identity Provider.
        /// </summary>
        /// <param name="userModel">The user data.</param>
        /// <returns>200 ok in case of success.</returns>
        [AllowAnonymous]
        [Route("admins")]
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