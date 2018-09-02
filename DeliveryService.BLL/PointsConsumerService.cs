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
    /// This service encapsulates the business logic of reading Point data.
    /// </summary>
    public class PointsConsumerService : IPointsConsumerService
    {
        private IPointsRepository pointsRepository;

        public PointsConsumerService(IPointsRepository pointsRepository)
        {
            this.pointsRepository = pointsRepository;
        }

        /// <summary>
        /// Returns a specific point.
        /// </summary>
        /// <param name="pointId">The point Id</param>
        /// <returns>The requested point. If point does not exist, returns null.</returns>
        public PointDTO GetPoint(int pointId)
        {
            PointDTO point = null;

            if (pointId > 0)
            {
                point = this.pointsRepository.Get(pointId);
            }

            return point;
        }

        /// <summary>
        /// Returns the list of all points.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PointDTO> GetPoints()
        {
            return this.pointsRepository.ListAll();
        }
    }
}
