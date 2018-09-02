using AutoMapper;
using DeliveryService.Common;
using DeliveryService.Common.DTOs;
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

        public RouteDTO Get(int routeId)
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
                            DestinationId = r.Destination.Id,
                            OriginName = r.Origin.Name,
                            DestinationName = r.Destination.Name
                        })
                        .Where(p => p.Id.Equals(routeId))
                        .SingleOrDefault<RouteDTO>();
            }

            return route;
        }

        public IEnumerable<RouteDTO> ListAll()
        {
            IList<RouteDTO> routes = new List<RouteDTO>();

            using (var dbContext = new DeliveryServiceDbContext())
            {
                routes = dbContext.Routes
                        .Select(r => new RouteDTO()
                        {
                            Id = r.Id,
                            Cost = r.Cost,
                            Minutes = r.Minutes,
                            OriginId = r.Origin.Id,
                            DestinationId = r.Destination.Id,
                            OriginName = r.Origin.Name,
                            DestinationName = r.Destination.Name
                        })
                        .ToList<RouteDTO>();
            }

            return routes;
        }

        public RouteDTO Save(RouteDTO route)
        {
            RouteDTO savedRoute = null;

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

                savedRoute = typeMapper.Map<Route, RouteDTO>(routeEntity);
            }

            return savedRoute;
        }

        public void Delete(int routeId)
        {
            using (var context = new DeliveryServiceDbContext())
            {
                Route routeEntity = context.Routes.Find(routeId);

                if (routeEntity != null)
                {
                    context.Entry<Route>(routeEntity).State = EntityState.Deleted;
                    context.SaveChanges();
                }
                else
                {
                    throw new ArgumentNullException("routeId", "The referenced point does not exist.");
                }
            }
        }
    }
}
