using DeliveryService.Common.DTOs;
using DeliveryService.Common.Interfaces.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DeliveryService.WebApi.Controllers
{
    [RoutePrefix("api/v1/routes")]
    public class RoutesController : ApiController
    {
        private IRoutesConsumerService routesConsumerService;
        private IRoutesAdminService routesAdminService;

        public RoutesController(IRoutesConsumerService consumerService, IRoutesAdminService adminService)
        {
            this.routesConsumerService = consumerService;
            this.routesAdminService = adminService;
        }

        /// <summary>
        /// Returns the list of all routes.
        /// </summary>
        /// <returns></returns>
        [Route("")]
        public IEnumerable<RouteDTO> Get()
        {
            return this.routesConsumerService.GetRoutes() ?? new List<RouteDTO>();
        }

        /// <summary>
        /// Returns the route with a specific Id.
        /// </summary>
        /// <param name="id">Route Id</param>
        /// <returns>The route with the specified Id. If the route could not be found, 400 bad request is returned.</returns>
        [Route("{id:int}")]
        public RouteDTO Get(int id)
        {
            RouteDTO route = this.routesConsumerService.GetRoute(id);

            if (route == null)
            {
                HttpResponseMessage message = new HttpResponseMessage()
                {
                    Content = new StringContent("Route Id not found."),
                    StatusCode = HttpStatusCode.BadRequest
                };

                throw new HttpResponseException(message);
            }

            return route;
        }

        /// <summary>
        /// Creates a new route object.
        /// </summary>
        /// <param name="route">The route object.</param>
        /// <returns>The newly created route object. If the route object is invalid, a 400 bad request is returned.</returns>
        [Authorize(Roles = "Administrator")]
        [Route("")]
        public IHttpActionResult Post([FromBody]RouteDTO route)
        {
            IHttpActionResult actionResult = null;

            if (route.Id > 0)
            {
                return BadRequest("Route Id cannot be defined for new entity.");
            }

            if (ModelState.IsValid)
            {
                RouteDTO savedRoute = this.routesAdminService.CreateRoute(route);
                actionResult = Ok<RouteDTO>(savedRoute);
            }
            else
            {
                actionResult = BadRequest(ModelState);
            }

            return actionResult;
        }


        /// <summary>
        /// Updates an existing route.
        /// </summary>
        /// <param name="route">The route object to be updated.</param>
        /// <returns>The updated route object.</returns>
        [Authorize(Roles = "Administrator")]
        [Route("")]
        public IHttpActionResult Put([FromBody]RouteDTO route)
        {
            IHttpActionResult actionResult = null;

            if (route.Id <= 0)
            {
                return BadRequest("Route Id must be greater than 0.");
            }

            if (ModelState.IsValid)
            {
                RouteDTO savedRoute = this.routesAdminService.UpdateRoute(route);

                actionResult = Ok<RouteDTO>(savedRoute);
            }
            else
            {
                actionResult = BadRequest(ModelState);
            }

            return actionResult;
        }

        /// <summary>
        /// Deletes an existing route.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>200 ok with no content if route is deleted.</returns>
        [Authorize(Roles = "Administrator")]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            IHttpActionResult actionResult = null;

            if (id <= 0)
            {
                return BadRequest("Route Id must be greater than 0.");
            }

            this.routesAdminService.DeleteRoute(id);

            return actionResult;
        }

        /// <summary>
        /// Returns the list of all possible paths from origin to destination provided that the path is not a direct path.
        /// The path must have at least 3 nodes.
        /// </summary>
        /// <param name="originId">The origin point</param>
        /// <param name="destinationId">The destination point</param>
        /// <returns>A list of paths.</returns>
        [Route("origin/{originId}/destination/{destinationId}")]
        [HttpGet]
        public IEnumerable<PathInfoDTO> GetNonDirectPaths(int originId, int destinationId)
        {
            return this.routesConsumerService.GetPaths(originId, destinationId, 3) ?? new List<PathInfoDTO>();
        }
    }
}