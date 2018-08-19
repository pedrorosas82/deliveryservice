using DeliveryService.BLL.Interfaces;
using DeliveryService.Common.DTOs;
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
            return this.routesConsumerService.GetRoutes();
        }

        // GET api/<controller>/5
        public RouteDTO Get(int id)
        {
            return this.routesConsumerService.GetRoute(id);
        }

        // POST api/<controller>
        public void Post([FromBody]RouteDTO route)
        {
            this.routesAdminService.CreateRoute(route);
        }

        // PUT api/<controller>/5
        public void Put([FromBody]RouteDTO route)
        {
            this.routesAdminService.UpdateRoute(route);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            this.routesAdminService.DeleteRoute(id);
        }

        // GET api/<<controller>>/origin/1/destination/2
        [Route("api/routes/origin/{originId}/destination/{destinationId}")]
        [HttpGet]
        public IEnumerable<PathInfoDTO> GetNonDirectPaths(int originId, int destinationId)
        {
            return this.routesConsumerService.GetPaths(originId, destinationId, 3);
        }
    }
}