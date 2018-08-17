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
    public class PointsController : ApiController
    {
        private IPointsConsumerService pointsConsumerService;
        private IPointsAdminService pointsAdminService;

        public PointsController(IPointsConsumerService consumerService, IPointsAdminService adminService)
        {
            this.pointsConsumerService = consumerService;
            this.pointsAdminService = adminService;
        }

        // GET api/<controller>
        public IEnumerable<PointDTO> Get()
        {
            return this.pointsConsumerService.GetPoints();
        }

        // GET api/<controller>/5
        public PointDTO Get(int id)
        {
            return this.pointsConsumerService.GetPoint(id);
        }

        // POST api/<controller>
        public void Post([FromBody]PointDTO point)
        {
            this.pointsAdminService.CreatePoint(point);
        }

        // PUT api/<controller>/5
        public void Put([FromBody]PointDTO point)
        {
            this.pointsAdminService.UpdatePoint(point);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            this.pointsAdminService.DeletePoint(id);
        }
    }
}