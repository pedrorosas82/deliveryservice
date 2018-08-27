using DeliveryService.Common.DTOs;
using DeliveryService.Common.Interfaces.BLL;
using DeliveryService.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.BLL
{
    public class RoutesCalculatorService : IRoutesCalculatorService
    {
        private IDictionary<int, IList<GraphWeightedNode>> routesGraph = new Dictionary<int, IList<GraphWeightedNode>>();

        public RoutesCalculatorService()
        {
            
        }

        public void LoadAllRoutes(IEnumerable<RouteDTO> allRoutes)
        {
            foreach (RouteDTO route in allRoutes)
            {
                if (this.routesGraph.ContainsKey(route.OriginId))
                {
                    routesGraph[route.OriginId].Add(new GraphWeightedNode()
                    {
                        PointId = route.DestinationId,
                        Cost = route.Cost,
                        Minutes = route.Minutes
                    });
                }
                else
                {
                    routesGraph.Add(route.OriginId, new List<GraphWeightedNode>()
                    {
                        new GraphWeightedNode()
                        {
                            PointId = route.DestinationId,
                            Cost = route.Cost,
                            Minutes = route.Minutes
                        }
                    });
                }

                if (!this.routesGraph.ContainsKey(route.DestinationId))
                {
                    routesGraph.Add(route.DestinationId, new List<GraphWeightedNode>());
                }
            }
        }

        public IEnumerable<GraphPath> GetAllPaths(int originId, int destinationId)
        {
            return GetAllPaths(originId, destinationId, 3);
        }

        public IEnumerable<GraphPath> GetAllPaths(int originId, int destinationId, int minimumPathNodes)
        {
            // https://medium.com/omarelgabrys-blog/path-finding-algorithms-f65a8902eb40
            // https://www.geeksforgeeks.org/print-paths-given-source-destination-using-bfs/

            IList<GraphPath> allPaths = new List<GraphPath>();
            Queue<GraphPath> queue = new Queue<GraphPath>();
            queue.Enqueue(new GraphPath()
            {
                PointIds = new List<int>() { originId },
                Cost = 0,
                Minutes = 0
            });

            while (queue.Count > 0)
            {
                GraphPath currentPath = queue.Dequeue();

                if (currentPath.PointIds.Last() == destinationId)
                {
                    if (currentPath.PointIds.Count >= minimumPathNodes)
                    {
                        allPaths.Add(currentPath);
                    }
                }

                foreach (GraphWeightedNode neighbour in this.routesGraph[currentPath.PointIds.Last()])
                {
                    if (!currentPath.PointIds.Contains(neighbour.PointId))
                    {
                        GraphPath newPath = new GraphPath(currentPath);
                        newPath.PointIds.Add(neighbour.PointId);
                        newPath.Cost = currentPath.Cost + neighbour.Cost;
                        newPath.Minutes = currentPath.Minutes + neighbour.Minutes;

                        queue.Enqueue(newPath);
                    }
                }
            }

            return allPaths;
        }
    }
}
