using DeliveryService.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.BLL.Interfaces
{
    public interface IRoutesAdminService
    {
        void CreateRoute(RouteDTO route);
        void UpdateRoute(RouteDTO route);
        void DeleteRoute(int routeId);
    }
}
