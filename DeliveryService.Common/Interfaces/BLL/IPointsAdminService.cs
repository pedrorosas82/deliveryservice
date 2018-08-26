using DeliveryService.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Common.Interfaces.BLL
{
    public interface IPointsAdminService
    {
        void CreatePoint(PointDTO point);
        void UpdatePoint(PointDTO point);
        void DeletePoint(int pointId);
    }
}
