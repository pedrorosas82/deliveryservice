using DeliveryService.Common.DTOs;
using DeliveryService.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Common.Interfaces.BLL
{
    public interface IRoutesCalculatorService
    {
        /// <summary>
        /// Loads all routes in memory to create a graph.
        /// </summary>
        /// <param name="allRoutes">The list of all routes</param>
        void LoadAllRoutes(IEnumerable<RouteDTO> allRoutes);

        /// <summary>
        /// Returns all non-direct paths from origin to destination.
        /// </summary>
        /// <param name="originId"></param>
        /// <param name="destinationId"></param>
        /// <returns>The list of paths.</returns>
        IEnumerable<GraphPath> GetAllNonDirectPaths(int originId, int destinationId);

        /// <summary>
        /// Returns the list of all paths from origin to destination with a minimum number of nodes.
        /// </summary>
        /// <param name="originId"></param>
        /// <param name="destinationId"></param>
        /// <param name="minimumPathNodes"></param>
        /// <returns></returns>
        IEnumerable<GraphPath> GetAllPaths(int originId, int destinationId, int minimumPathNodes);
    }
}
