using DeliveryService.Common;
using DeliveryService.Common.DTOs;
using DeliveryService.Common.Interfaces.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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
            return this.pointsConsumerService.GetPoints()?? new List<PointDTO>();
        }

        // GET api/<controller>/5
        public PointDTO Get(int id)
        {
            PointDTO point = this.pointsConsumerService.GetPoint(id);

            if (point == null)
            {
                HttpResponseMessage message = new HttpResponseMessage()
                {
                    Content = new StringContent("Point Id not found."),
                    StatusCode = HttpStatusCode.BadRequest
                };
                

                throw new HttpResponseException(message);
            }

            return point;
        }

        // POST api/<controller>
        public IHttpActionResult Post([FromBody]PointDTO point)
        {
            IHttpActionResult actionResult = null;

            if (point.Id > 0)
            {
                return BadRequest("Point Id cannot be defined for new entity.");
            }

            if (ModelState.IsValid)
            {
                PointDTO savedPoint = this.pointsAdminService.CreatePoint(point);
                actionResult = Ok<PointDTO>(savedPoint);
            }
            else
            {
                actionResult = BadRequest(ModelState);
            }

            return actionResult;
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put([FromBody]PointDTO point)
        {
            IHttpActionResult actionResult = null;

            if (point.Id <= 0)
            {
                return BadRequest("Point Id must be greater than 0.");
            }

            if (ModelState.IsValid)
            {
                PointDTO savedPoint = this.pointsAdminService.UpdatePoint(point);

                actionResult = Ok<PointDTO>(savedPoint);
            }
            else
            {
                actionResult = BadRequest(ModelState);
            }

            return actionResult;
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            this.pointsAdminService.DeletePoint(id);
        }
    }
}