using DeliveryService.Common;
using DeliveryService.Common.DTOs;
using DeliveryService.Common.Interfaces.BLL;
using DeliveryService.Common.Interfaces.DAL;
using DeliveryService.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.BLL
{
    /// <summary>
    /// This service encapsulates the business logic of reading route data.
    /// </summary>
    public class RoutesConsumerService : IRoutesConsumerService
    {
        private IRoutesRepository routesRepository;
        private IPointsRepository pointsRepository;
        private IRoutesCalculatorService routesCalculator;

        public RoutesConsumerService(IRoutesRepository routesRepository, IPointsRepository pointsRepository, IRoutesCalculatorService routesCalculator)
        {
            this.routesRepository = routesRepository;
            this.pointsRepository = pointsRepository;
            this.routesCalculator = routesCalculator;

            this.routesCalculator.LoadAllRoutes(this.routesRepository.ListAll());
        }

        /// <summary>
        /// Returns the list of all paths from an origin point to a destination point.
        /// </summary>
        /// <param name="originId">The origin Id.</param>
        /// <param name="destinationId">The destination Id.</param>
        /// <param name="minimumNumberOfPoints">The minimum number of nodes to consider the route.</param>
        /// <returns></returns>
        public IEnumerable<PathInfoDTO> GetPaths(int originId, int destinationId, int minimumNumberOfPoints)
        {
            IEnumerable<PathInfoDTO> paths = new List<PathInfoDTO>();
            IEnumerable<PointDTO> allPoints = this.pointsRepository.ListAll()?? new List<PointDTO>();

            if (!allPoints.Any(x => x.Id.Equals(originId)))
            {
                throw new ArgumentException(String.Format("Origin Id {0} does not exist.", originId));
            }

            if (!allPoints.Any(x => x.Id.Equals(destinationId)))
            {
                throw new ArgumentException(String.Format("Destination Id {0} does not exist.", destinationId));
            }

            IEnumerable<GraphPath> graphPaths = this.routesCalculator.GetAllPaths(originId, destinationId, minimumNumberOfPoints)?? new List<GraphPath>();

            paths = buildPathInfoList(graphPaths, allPoints);

            return paths;
        }

        /// <summary>
        /// Returns a specific route.
        /// </summary>
        /// <param name="routeId">The route Id.</param>
        /// <returns>The route requested. If no match is found, returns null.</returns>
        public RouteDTO GetRoute(int routeId)
        {
            RouteDTO route = null;

            if (routeId > 0)
            {
                route = this.routesRepository.Get(routeId);
            }

            return route;
        }

        /// <summary>
        /// Returns the list of all routes.
        /// </summary>
        /// <returns>The list of routes.</returns>
        public IEnumerable<RouteDTO> GetRoutes()
        {
            return this.routesRepository.ListAll();
        }

        /// <summary>
        /// This methods attaches additional information - namely the point names, to the list of paths.
        /// </summary>
        /// <param name="graphPaths">The paths found in the graph.</param>
        /// <param name="allPoints">The list of all the points.</param>
        /// <returns>A list of paths with the point names attached.</returns>
        private IEnumerable<PathInfoDTO> buildPathInfoList(IEnumerable<GraphPath> graphPaths, IEnumerable<PointDTO> allPoints)
        {
            IList<PathInfoDTO> pathInfos = new List<PathInfoDTO>();

            foreach (GraphPath path in graphPaths)
            {
                PathInfoDTO pathInfo = new PathInfoDTO()
                {
                    PointIds = path.PointIds,
                    PointNames = new List<string>(),
                    Cost = path.Cost,
                    Minutes = path.Minutes
                };
                
                foreach (int pointId in path.PointIds)
                {
                    string pointName = allPoints.Single(p => p.Id.Equals(pointId)).Name;
                    pathInfo.PointNames.Add(pointName);
                }

                pathInfos.Add(pathInfo);
            }

            return pathInfos;
        }
    }
}
