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
    /// <summary>
    /// This class is a service that encapsulates the business logic related to create/update/delete operations for Routes.
    /// </summary>
    public class RoutesAdminService : IRoutesAdminService
    {
        private IRoutesRepository routesRepository;

        public RoutesAdminService(IRoutesRepository routesRepository)
        {
            this.routesRepository = routesRepository;
        }

        /// <summary>
        /// Creates a new route.
        /// </summary>
        /// <param name="route">The route data.</param>
        /// <returns>The newly created route.</returns>
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

        /// <summary>
        /// Updates an existing route.
        /// </summary>
        /// <param name="route">The route data.</param>
        /// <returns>The updated route.</returns>
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

        /// <summary>
        /// Deletes an existing route.
        /// </summary>
        /// <param name="routeId">The route Id.</param>
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