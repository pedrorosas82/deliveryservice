﻿using DeliveryService.BLL.Helpers;
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
        private IPointsRepository pointsRepository;

        public RoutesConsumerService(IRoutesRepository routesRepository, IPointsRepository pointsRepository)
        {
            this.routesRepository = routesRepository;
            this.pointsRepository = pointsRepository;
        }

        public IEnumerable<PathInfoDTO> GetNonDirectPaths(int originId, int destinationId)
        {
            IEnumerable<PathInfoDTO> paths = new List<PathInfoDTO>();

            IEnumerable<RouteDTO> allRoutes = this.routesRepository.GetRoutes();
            RoutesGraph routesGraph = new RoutesGraph(allRoutes);

            IEnumerable<PathDTO> pathDTOs = routesGraph.GetAllPaths(originId, destinationId, 3);
            IEnumerable<PointDTO> allPoints = this.pointsRepository.GetPoints();

            return this.buildPathInfoList(pathDTOs, allPoints, allRoutes);
        }

        public RouteDTO GetRoute(int routeId)
        {
            return this.routesRepository.GetRoute(routeId);
        }

        public IEnumerable<RouteDTO> GetRoutes()
        {
            return this.routesRepository.GetRoutes();
        }


        private IEnumerable<PathInfoDTO> buildPathInfoList(IEnumerable<PathDTO> pathDTOs, IEnumerable<PointDTO> allPoints, IEnumerable<RouteDTO> allRoutes)
        {
            IList<PathInfoDTO> pathInfos = new List<PathInfoDTO>();

            foreach (PathDTO path in pathDTOs)
            {
                PathInfoDTO pathInfo = new PathInfoDTO();
                pathInfo.PointIds = path.PointIds;
                pathInfo.PointNames = new List<string>();
                
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
