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
    public class PointsAdminService : IPointsAdminService
    {
        private IPointsRepository pointsRepository;

        public PointsAdminService(IPointsRepository pointsRepository)
        {
            this.pointsRepository = pointsRepository;
        }

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
