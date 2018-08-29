using DeliveryService.Common;
using DeliveryService.Common.DTOs;
using DeliveryService.Common.Interfaces.BLL;
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

        public RouteDTO CreateRoute(RouteDTO route)
        {
            RouteDTO savedRoute = null;

            if (route.Id == 0)
            {
                savedRoute = this.routesRepository.Save(route);
            }
            else
            {
                throw new ArgumentException("Route Id is assigned automatically. Cannot create route with the Id assigned.");
            }

            return savedRoute;
        }

        public RouteDTO UpdateRoute(RouteDTO route)
        {
            RouteDTO savedRoute = null;

            if (route.Id > 0)
            {
                savedRoute = this.routesRepository.Save(route);
            }
            else
            {
                throw new ArgumentException("Route Id must be an integer greater than 0.");
            }

            return savedRoute;
        }

        public void DeleteRoute(int routeId)
        {
            if (routeId > 0)
            {
                this.routesRepository.Delete(routeId);
            }
            else
            {
                throw new ArgumentException("Route Id must be an integer greater than 0.");
            }
        }
    }
}
