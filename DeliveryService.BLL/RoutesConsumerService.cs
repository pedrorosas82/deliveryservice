using DeliveryService.BLL.Helpers;
using DeliveryService.BLL.Interfaces;
using DeliveryService.Common;
using DeliveryService.Common.DTOs;
using DeliveryService.Common.Interfaces.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.BLL
{
    public class RoutesConsumerService : IRoutesConsumerService
    {
        private IRoutesRepository routesRepository;

        public RoutesConsumerService(IRoutesRepository routesRepository)
        {
            this.routesRepository = routesRepository;
        }

        public IEnumerable<PathDTO> GetNonDirectPaths(int originId, int destinationId)
        {
            IEnumerable<PathDTO> paths = new List<PathDTO>();

            IEnumerable<RouteDTO> allRoutes = this.routesRepository.GetRoutes();
            RoutesGraph routesGraph = new RoutesGraph(allRoutes);

            return routesGraph.GetAllPaths(originId, destinationId);
        }

        public RouteDTO GetRoute(int routeId)
        {
            return this.routesRepository.GetRoute(routeId);
        }

        public IEnumerable<RouteDTO> GetRoutes()
        {
            return this.routesRepository.GetRoutes();
        }
    }
}
