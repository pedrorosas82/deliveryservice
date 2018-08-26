using DeliveryService.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Common.Interfaces.BLL
{
    public interface IRoutesConsumerService
    {
        IEnumerable<RouteDTO> GetRoutes();
        RouteDTO GetRoute(int routeId);
        IEnumerable<PathInfoDTO> GetPaths(int originId, int destinationId, int minimumNodes);
    }
}
