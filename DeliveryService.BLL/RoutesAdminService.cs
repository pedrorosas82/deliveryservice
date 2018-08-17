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
    public class RoutesAdminService : IRoutesAdminService
    {
        private IRoutesRepository routesRepository;

        public RoutesAdminService(IRoutesRepository routesRepository)
        {
            this.routesRepository = routesRepository;
        }

        public void CreateRoute(RouteDTO route)
        {
            this.routesRepository.CreateRoute(route);
        }

        public void DeleteRoute(int routeId)
        {
            this.routesRepository.DeleteRoute(routeId);
        }

        public void UpdateRoute(RouteDTO route)
        {
            this.routesRepository.UpdateRoute(route);
        }
    }
}
