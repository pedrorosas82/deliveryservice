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
    public class PointsAdminService : IPointsAdminService
    {
        private IPointsRepository pointsRepository;

        public PointsAdminService(IPointsRepository pointsRepository)
        {
            this.pointsRepository = pointsRepository;
        }

        public void CreatePoint(PointDTO point)
        {
            this.pointsRepository.CreatePoint(point);
        }

        public void DeletePoint(int pointId)
        {
            this.pointsRepository.DeletePoint(pointId);
        }

        public void UpdatePoint(PointDTO point)
        {
            this.pointsRepository.UpdatePoint(point);
        }
    }
}
