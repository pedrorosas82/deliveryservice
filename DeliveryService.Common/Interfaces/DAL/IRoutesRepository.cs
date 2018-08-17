﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Common.Interfaces.DAL
{
    public interface IRoutesRepository
    {
        IEnumerable<RouteDTO> GetRoutes();
        RouteDTO GetRoute(int routeId);

        void CreateRoute(RouteDTO route);
        void UpdateRoute(RouteDTO route);
        void DeleteRoute(int routeId);
    }
}
