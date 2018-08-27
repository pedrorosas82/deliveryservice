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
        }

        public IEnumerable<PathInfoDTO> GetPaths(int originId, int destinationId, int minimumNodes)
        {
            IEnumerable<PathInfoDTO> paths = new List<PathInfoDTO>();

            this.routesCalculator.LoadAllRoutes(this.routesRepository.GetRoutes());
            IEnumerable<GraphPath> graphPaths = this.routesCalculator.GetAllPaths(originId, destinationId, minimumNodes);
            IEnumerable<PointDTO> allPoints = this.pointsRepository.GetPoints();

            return this.buildPathInfoList(graphPaths, allPoints);
        }

        public RouteDTO GetRoute(int routeId)
        {
            RouteDTO route = null;

            if (routeId > 0)
            {
                route = this.routesRepository.GetRoute(routeId);
            }
            else
            {
                throw new ArgumentException("Route Id must be an integer greater than 0.");
            }

            return route;
        }

        public IEnumerable<RouteDTO> GetRoutes()
        {
            return this.routesRepository.GetRoutes();
        }


        private IEnumerable<PathInfoDTO> buildPathInfoList(IEnumerable<GraphPath> graphPaths, IEnumerable<PointDTO> allPoints)
        {
            IList<PathInfoDTO> pathInfos = new List<PathInfoDTO>();

            foreach (GraphPath path in graphPaths)
            {
                PathInfoDTO pathInfo = new PathInfoDTO();
                pathInfo.PointIds = path.PointIds;
                pathInfo.PointNames = new List<string>();
                pathInfo.Cost = path.Cost;
                pathInfo.Minutes = path.Minutes;
                
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
