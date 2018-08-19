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
    public class RoutesAdminService : IRoutesAdminService
    {
        private IRoutesRepository routesRepository;

        public RoutesAdminService(IRoutesRepository routesRepository)
        {
            this.routesRepository = routesRepository;
        }

        public void CreateRoute(RouteDTO route)
        {
            if (route.Id == 0)
            {
                this.routesRepository.SaveRoute(route);
            }
            else
            {
                throw new ApplicationException("Route Id is assigned automatically. Cannot create route with the Id assigned.");
            }
        }

        public void UpdateRoute(RouteDTO route)
        {
            if (route.Id > 0)
            {
                this.routesRepository.SaveRoute(route);
            }
            else
            {
                throw new ApplicationException("Route Id must be specified.");
            }
        }

        public void DeleteRoute(int routeId)
        {
            if (routeId > 0)
            {
                this.routesRepository.DeleteRoute(routeId);
            }
            else
            {
                throw new ApplicationException("Route Id must be specified");
            }
        }
    }
}
