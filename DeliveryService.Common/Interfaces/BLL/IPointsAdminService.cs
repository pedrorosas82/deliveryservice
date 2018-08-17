using DeliveryService.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.BLL.Interfaces
{
    public interface IPointsAdminService
    {
        void CreatePoint(PointDTO route);
        void UpdatePoint(PointDTO route);
        void DeletePoint(int pointId);
    }
}
