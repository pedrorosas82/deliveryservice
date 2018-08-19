using DeliveryService.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.BLL.Interfaces
{
    public interface IRoutesAdminService
    {
        void UpdateRoute(RouteDTO route);
        void CreateRoute(RouteDTO route);
        void DeleteRoute(int routeId);
    }
}
