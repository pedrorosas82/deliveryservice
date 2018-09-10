using DeliveryService.Common.DTOs;
using DeliveryService.Common.Interfaces.DAL;
using Neo4j.Driver.V1;
using System;
using System.Collections.Generic;

namespace DeliveryService.Neo4j.DAL.Repositories
{
    public class PointsRepository : IPointsRepository
    {
        private IDriver neo4jDriver;

        public PointsRepository(IDriver neo4jDriver)
        {
            this.neo4jDriver = neo4jDriver;
        }

        public void Delete(int pointId)
        {
            using (var session = this.neo4jDriver.Session())
            {
                // TODO: add logic
            }

            throw new NotImplementedException();
        }

        public PointDTO Get(int pointId)
        {
            using (var session = this.neo4jDriver.Session())
            {
                // TODO: add logic
            }

            throw new NotImplementedException();
        }

        public IEnumerable<PointDTO> ListAll()
        {
            using (var session = this.neo4jDriver.Session())
            {
                // TODO: add logic
            }

            throw new NotImplementedException();
        }

        public PointDTO Save(PointDTO point)
        {
            using (var session = this.neo4jDriver.Session())
            {
                // TODO: add logic
            }

            throw new NotImplementedException();
        }
    }
}
