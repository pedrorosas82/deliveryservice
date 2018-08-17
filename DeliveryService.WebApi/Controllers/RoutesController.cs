using DeliveryService.BLL.Interfaces;
using DeliveryService.Common;
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
    }
}