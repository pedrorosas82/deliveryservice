using DeliveryService.Common.DTOs;
using DeliveryService.Common.Interfaces.DAL;
using Neo4j.Driver.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Neo4j.DAL.Repositories
{
    public class RoutesRepository : IRoutesRepository
    {
        private IDriver neo4jDriver;

        public RoutesRepository(IDriver neo4jDriver)
        {
            this.neo4jDriver = neo4jDriver;
        }

        public void Delete(int routeId)
        {
            using (var session = this.neo4jDriver.Session())
            {
                // TODO: add logic
            }

            throw new NotImplementedException();
        }

        public RouteDTO Get(int routeId)
        {
            using (var session = this.neo4jDriver.Session())
            {
                // TODO: add logic
            }

            throw new NotImplementedException();
        }

        public IEnumerable<RouteDTO> ListAll()
        {
            using (var session = this.neo4jDriver.Session())
            {
                // TODO: add logic
            }

            throw new NotImplementedException();
        }

        public RouteDTO Save(RouteDTO route)
        {
            using (var session = this.neo4jDriver.Session())
            {
                // TODO: add logic
            }

            throw new NotImplementedException();
        }
    }
}
