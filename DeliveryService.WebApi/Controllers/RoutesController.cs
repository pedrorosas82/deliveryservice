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
    public class RoutesController : ApiController
    {
        private IRoutesConsumerService routesConsumerService;
        private IRoutesAdminService routesAdminService;

        public RoutesController(IRoutesConsumerService consumerService, IRoutesAdminService adminService)
        {
            this.routesConsumerService = consumerService;
            this.routesAdminService = adminService;
        }

        // GET api/<controller>
        public IEnumerable<RouteDTO> Get()
        {
            return this.routesConsumerService.GetRoutes() ?? new List<RouteDTO>();
        }

        // GET api/<controller>/5
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

        // POST api/<controller>
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

        // PUT api/<controller>/5
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

        // DELETE api/<controller>/5
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

        // GET api/<<controller>>/origin/1/destination/2
        [Route("api/routes/origin/{originId}/destination/{destinationId}")]
        [HttpGet]
        public IEnumerable<PathInfoDTO> GetNonDirectPaths(int originId, int destinationId)
        {
            return this.routesConsumerService.GetPaths(originId, destinationId, 3) ?? new List<PathInfoDTO>();
        }
    }
}