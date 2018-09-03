using DeliveryService.Common.DTOs;
using DeliveryService.Common.Interfaces.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Neo4j.DAL.Repositories
{
    public class RoutesRepository : IRoutesRepository
    {
        public void Delete(int routeId)
        {
            throw new NotImplementedException();
        }

        public RouteDTO Get(int routeId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RouteDTO> ListAll()
        {
            throw new NotImplementedException();
        }

        public RouteDTO Save(RouteDTO route)
        {
            throw new NotImplementedException();
        }
    }
}
