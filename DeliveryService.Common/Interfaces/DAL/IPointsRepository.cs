using DeliveryService.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Common.Interfaces.DAL
{
    public interface IPointsRepository
    {
        IEnumerable<PointDTO> GetPoints();
        PointDTO GetPoint(int id);

        void SavePoint(PointDTO point);
        void DeletePoint(int pointId);
    }
}
