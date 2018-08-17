using DeliveryService.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.BLL.Interfaces
{
    public interface IRoutesConsumerService
    {
        IEnumerable<RouteDTO> GetRoutes();
        RouteDTO GetRoute(int routeId);
    }
}
