using DeliveryService.Common.DTOs;
using DeliveryService.Common.Interfaces.BLL;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DeliveryService.WebApi.Controllers
{
    [RoutePrefix("api/v1/points")]
    public class PointsController : ApiController
    {
        private IPointsConsumerService pointsConsumerService;
        private IPointsAdminService pointsAdminService;

        public PointsController(IPointsConsumerService consumerService, IPointsAdminService adminService)
        {
            this.pointsConsumerService = consumerService;
            this.pointsAdminService = adminService;
        }

        /// <summary>
        /// Returns the list of all points.
        /// </summary>
        /// <returns></returns>
        [Route("")]
        public IEnumerable<PointDTO> Get()
        {
            return this.pointsConsumerService.GetPoints()?? new List<PointDTO>();
        }

        /// <summary>
        /// Returns the point with a specific Id.
        /// </summary>
        /// <param name="id">Point Id</param>
        /// <returns>The point with the specified Id. If the point could not be found, 400 bad request is returned.</returns>
        [Route("{id:int}")]
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

        /// <summary>
        /// Creates a new point object.
        /// </summary>
        /// <param name="point">The point object.</param>
        /// <returns>The newly created point object. If the point object is invalid, a 400 bad request is returned.</returns>
        [Authorize(Roles = "Administrator")]
        [Route("")]
        public IHttpActionResult Post([FromBody]PointDTO point)
        {
            if (point.Id > 0)
            {
                return BadRequest("Point Id cannot be defined for new entity.");
            }

            IHttpActionResult actionResult = BadRequest();

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

        /// <summary>
        /// Updates an existing point.
        /// </summary>
        /// <param name="point">The point object to be updated.</param>
        /// <returns>The updated point object.</returns>
        [Authorize(Roles = "Administrator")]
        [Route("")]
        public IHttpActionResult Put([FromBody]PointDTO point)
        {
            if (point.Id <= 0)
            {
                return BadRequest("Point Id must be greater than 0.");
            }

            IHttpActionResult actionResult = BadRequest();

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

        /// <summary>
        /// Deletes an existing point.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>200 ok with no content if point is deleted.</returns>
        [Authorize(Roles = "Administrator")]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Point Id must be greater than 0.");
            }

            IHttpActionResult actionResult = BadRequest();

            this.pointsAdminService.DeletePoint(id);
            actionResult = Ok();

            return actionResult;
        }
    }
}