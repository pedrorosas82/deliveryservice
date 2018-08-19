using DeliveryService.BLL.Interfaces;
using DeliveryService.Common;
using DeliveryService.Common.DTOs;
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
            if (point.Id == 0)
            {
                this.pointsRepository.SavePoint(point);
            }
            else
            {
                throw new ApplicationException("Point Id is assigned automatically. Cannot create point with the Id assigned.");
            }
        }

        public void UpdatePoint(PointDTO point)
        {
            if (point.Id > 0)
            {
                this.pointsRepository.SavePoint(point);
            }
            else
            {
                throw new ApplicationException("Point Id must be specified.");
            }
        }

        public void DeletePoint(int pointId)
        {
            this.pointsRepository.DeletePoint(pointId);
        }
    }
}
