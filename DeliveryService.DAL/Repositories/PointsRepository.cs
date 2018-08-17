using DeliveryService.Common;
using DeliveryService.Common.Interfaces;
using DeliveryService.Common.Interfaces.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.DAL.Repositories
{
    public class PointsRepository : IPointsRepository
    {
        public PointDTO GetPoint(int pointId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PointDTO> GetPoints()
        {
            throw new NotImplementedException();
        }

        public void CreatePoint(PointDTO point)
        {
            throw new NotImplementedException();
        }

        public void DeletePoint(int pointId)
        {
            throw new NotImplementedException();
        }

        public void UpdatePoint(PointDTO point)
        {
            throw new NotImplementedException();
        }
    }
}
