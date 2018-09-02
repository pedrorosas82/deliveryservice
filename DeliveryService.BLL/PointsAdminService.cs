using DeliveryService.Common;
using DeliveryService.Common.DTOs;
using DeliveryService.Common.Interfaces.BLL;
using DeliveryService.Common.Interfaces.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.BLL
{
    /// <summary>
    /// This class is a service that encapsulates the business logic related to create/update/delete operations for Points.
    /// </summary>
    public class PointsAdminService : IPointsAdminService
    {
        private IPointsRepository pointsRepository;

        public PointsAdminService(IPointsRepository pointsRepository)
        {
            this.pointsRepository = pointsRepository;
        }

        /// <summary>
        /// Creates a new point.
        /// </summary>
        /// <param name="point">The point data.</param>
        /// <returns>The created point.</returns>
        public PointDTO CreatePoint(PointDTO point)
        {
            PointDTO savedPoint = null;

            if (point.Id == 0)
            {
                savedPoint = this.pointsRepository.Save(point);
            }
            else
            {
                throw new ArgumentException("Point Id is assigned automatically. Cannot create point with the Id assigned.");
            }

            return savedPoint;
        }

        /// <summary>
        /// Updates an existing point.
        /// </summary>
        /// <param name="point">The new point data.</param>
        /// <returns>The updated point.</returns>
        public PointDTO UpdatePoint(PointDTO point)
        {
            PointDTO savedPoint = null;

            if (point.Id > 0)
            {
                savedPoint = this.pointsRepository.Save(point);
            }
            else
            {
                throw new ArgumentException("Point Id must be an integer greater than 0.");
            }

            return savedPoint;
        }

        /// <summary>
        /// Deletes an existing point.
        /// </summary>
        /// <param name="pointId">The point Id.</param>
        public void DeletePoint(int pointId)
        {
            if (pointId > 0)
            {
                this.pointsRepository.Delete(pointId);
            }
            else
            {
                throw new ArgumentException("Point Id must be an integer greater than 0.");
            }
        }
    }
}
