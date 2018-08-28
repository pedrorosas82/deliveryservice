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
    public class PointsConsumerService : IPointsConsumerService
    {
        private IPointsRepository pointsRepository;

        public PointsConsumerService(IPointsRepository pointsRepository)
        {
            this.pointsRepository = pointsRepository;
        }

        public PointDTO GetPoint(int pointId)
        {
            PointDTO point = null;

            if (pointId > 0)
            {
                point = this.pointsRepository.Get(pointId);
            }

            return point;
        }

        public IEnumerable<PointDTO> GetPoints()
        {
            return this.pointsRepository.ListAll();
        }
    }
}
