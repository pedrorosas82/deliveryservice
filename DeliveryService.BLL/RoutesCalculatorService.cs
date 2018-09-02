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
    /// <summary>
    /// This service encapsulates the logic for route calculation.
    /// </summary>
    public class RoutesCalculatorService : IRoutesCalculatorService
    {
        /// <summary>
        /// The graph represented as an adjacency list.
        /// </summary>
        private IDictionary<int, IList<GraphWeightedNode>> routesGraph = new Dictionary<int, IList<GraphWeightedNode>>();

        public RoutesCalculatorService(IEnumerable<RouteDTO> allRoutes)
        {
            this.loadAllRoutes(allRoutes);
        }

        /// <summary>
        /// Returns all possible paths from origin to destination provided that they are not direct paths.
        /// </summary>
        /// <param name="originId">The origin point.</param>
        /// <param name="destinationId">The destination point.</param>
        /// <returns>The list of paths from origin to destination.</returns>
        public IEnumerable<GraphPath> GetAllNonDirectPaths(int originId, int destinationId)
        {
            return GetAllPaths(originId, destinationId, 3);
        }

        /// <summary>
        /// Returns all possible paths from origin to destination.
        /// The algorithm used for graph traversal is a BFS (breadth first search).
        /// </summary>
        /// <param name="originId">The origin point.</param>
        /// <param name="destinationId">The destination point.</param>
        /// <param name="minimumPathNodes">The minimum number of nodes to consider on the result.</param>
        /// <returns>Returns all paths from origin to destination that contain a minimum number of nodes.</returns>
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

        /// <summary>
        /// Builds the graph data.
        /// It takes all the routes and builds an adjacency list to represent the graph.
        /// </summary>
        /// <param name="allRoutes">The list of all routes.</param>
        public void LoadAllRoutes(IEnumerable<RouteDTO> allRoutes)
        {
            this.loadAllRoutes(allRoutes);
        }


        private void loadAllRoutes(IEnumerable<RouteDTO> allRoutes)
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
    }
}
