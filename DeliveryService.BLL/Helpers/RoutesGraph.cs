using DeliveryService.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.BLL.Helpers
{
    public class RoutesGraph
    {
        private IDictionary<int, IList<int>> routesGraph = new Dictionary<int, IList<int>>();

        public RoutesGraph(IEnumerable<RouteDTO> allRoutes)
        {
            foreach (RouteDTO route in allRoutes)
            {
                if (this.routesGraph.ContainsKey(route.OriginId))
                {
                    routesGraph[route.OriginId].Add(route.DestinationId);
                }
                else
                {
                    routesGraph.Add(route.OriginId, new List<int>() { route.DestinationId });
                }

                if (!this.routesGraph.ContainsKey(route.DestinationId))
                {
                    routesGraph.Add(route.DestinationId, new List<int>());
                }
            }
        }

        public IEnumerable<PathDTO> GetAllPaths(int originId, int destinationId)
        {
            // https://medium.com/omarelgabrys-blog/path-finding-algorithms-f65a8902eb40
            // https://www.geeksforgeeks.org/print-paths-given-source-destination-using-bfs/

            IList<PathDTO> allPaths = new List<PathDTO>();
            Queue<PathDTO> queue = new Queue<PathDTO>();
            queue.Enqueue(new PathDTO() { PointIds = new List<int>() { originId } });

            while (queue.Count > 0)
            {
                PathDTO currentPath = queue.Dequeue();

                if (currentPath.PointIds.Last() == destinationId)
                {
                    allPaths.Add(currentPath);
                }

                foreach (int neighbour in this.routesGraph[currentPath.PointIds.Last()])
                {
                    if (!currentPath.PointIds.Contains(neighbour))
                    {
                        PathDTO newPath = new PathDTO(currentPath);
                        newPath.PointIds.Add(neighbour);

                        queue.Enqueue(newPath);
                    }
                }
            }

            return allPaths;
        }
    }
}
