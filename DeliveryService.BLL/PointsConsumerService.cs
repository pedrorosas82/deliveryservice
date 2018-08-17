using DeliveryService.BLL.Interfaces;
using DeliveryService.Common;
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
            return this.pointsRepository.GetPoint(pointId);
        }

        public IEnumerable<PointDTO> GetPoints()
        {
            return this.pointsRepository.GetPoints();
        }
    }
}
