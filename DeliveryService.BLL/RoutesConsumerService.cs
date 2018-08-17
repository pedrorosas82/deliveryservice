using DeliveryService.BLL.Interfaces;
using DeliveryService.Common;
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
