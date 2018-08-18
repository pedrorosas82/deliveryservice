using AutoMapper;
using DeliveryService.Common;
using DeliveryService.Common.Interfaces.DAL;
using DeliveryService.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.DAL.Repositories
{
    public class RoutesRepository : IRoutesRepository
    {
        private IMapper typeMapper;

        public RoutesRepository(IMapper typeMapper)
        {
            this.typeMapper = typeMapper;
        }

        public RouteDTO GetRoute(int routeId)
        {
            RouteDTO route = null;

            using (var dbContext = new DeliveryServiceDbContext())
            {
                route = dbContext.Routes
                        .Select(r => new RouteDTO()
                        {
                            Id = r.Id,
                            Cost = r.Cost,
                            OriginId = r.Origin.Id,
                            DestinationId = r.Destination.Id
                        })
                        .Where(p => p.Id.Equals(routeId))
                        .SingleOrDefault<RouteDTO>();
            }

            return route;
        }

        public IEnumerable<RouteDTO> GetRoutes()
        {
            IList<RouteDTO> routes = new List<RouteDTO>();

            using (var dbContext = new DeliveryServiceDbContext())
            {
                routes = dbContext.Routes
                        .Select(r => new RouteDTO()
                        {
                            Id = r.Id,
                            Cost = r.Cost,
                            OriginId = r.Origin.Id,
                            DestinationId = r.Destination.Id
                        })
                        .ToList<RouteDTO>();
            }

            return routes;
        }

        public void SaveRoute(RouteDTO route)
        {
            using (var context = new DeliveryServiceDbContext())
            {
                Point sourceEntity = context.Points.Find(route.OriginId);
                Point destinationEntity = context.Points.Find(route.DestinationId);

                if (sourceEntity == null)
                {
                    throw new ApplicationException("Route source point does not exist.");
                }

                if (destinationEntity == null)
                {
                    throw new ApplicationException("Route destination point does not exist.");
                }

                Route routeEntity = new Route()
                {
                    Id = route.Id,
                    Origin = sourceEntity,
                    Destination = destinationEntity,
                    Cost = route.Cost,
                    Minutes = route.Minutes
                };

                if (routeEntity.Id == 0)
                {
                    context.Entry(routeEntity).State = EntityState.Added;
                }
                else
                {
                    context.Entry(routeEntity).State = EntityState.Modified;
                }

                context.SaveChanges();
            }
        }

        public void DeleteRoute(int routeId)
        {
            using (var context = new DeliveryServiceDbContext())
            {
                Route routeEntity = context.Routes.Find(routeId);

                context.Entry<Route>(routeEntity).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
    }
}
