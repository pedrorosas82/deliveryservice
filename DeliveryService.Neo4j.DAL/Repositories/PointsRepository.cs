using DeliveryService.Common.DTOs;
using DeliveryService.Common.Interfaces.DAL;
using System;
using System.Collections.Generic;

namespace DeliveryService.Neo4j.DAL.Repositories
{
    public class PointsRepository : IPointsRepository
    {
        public void Delete(int pointId)
        {
            throw new NotImplementedException();
        }

        public PointDTO Get(int pointId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PointDTO> ListAll()
        {
            throw new NotImplementedException();
        }

        public PointDTO Save(PointDTO point)
        {
            throw new NotImplementedException();
        }
    }
}
