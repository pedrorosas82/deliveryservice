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
        PointDTO CreatePoint(PointDTO point);
        PointDTO UpdatePoint(PointDTO point);
        void DeletePoint(int pointId);
    }
}
