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

        void CreatePoint(PointDTO point);
        void DeletePoint(int pointId);
        void UpdatePoint(PointDTO point);
    }
}
