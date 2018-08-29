using DeliveryService.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Common.Interfaces.BLL
{
    public interface IRoutesAdminService
    {
        RouteDTO UpdateRoute(RouteDTO route);
        RouteDTO CreateRoute(RouteDTO route);
        void DeleteRoute(int routeId);
    }
}
