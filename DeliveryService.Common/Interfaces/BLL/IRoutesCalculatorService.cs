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
        void LoadAllRoutes(IEnumerable<RouteDTO> allRoutes);
        IEnumerable<GraphPath> GetAllPaths(int originId, int destinationId);
        IEnumerable<GraphPath> GetAllPaths(int originId, int destinationId, int minimumPathNodes);
    }
}
