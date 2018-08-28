using AutoMapper;
using DeliveryService.Common;
using DeliveryService.Common.DTOs;
using DeliveryService.Common.Interfaces;
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
    public class PointsRepository : IPointsRepository
    {
        private IMapper typeMapper;

        public PointsRepository(IMapper typeMapper)
        {
            this.typeMapper = typeMapper;
        }

        public PointDTO Get(int pointId)
        {
            PointDTO point = null;

            using (var dbContext = new DeliveryServiceDbContext())
            {
                point = dbContext.Points
                        .Select(p => new PointDTO()
                        {
                            Id = p.Id,
                            Name = p.Name
                        })
                        .Where(p => p.Id.Equals(pointId))
                        .SingleOrDefault<PointDTO>();
            }

            return point;
        }

        public IEnumerable<PointDTO> ListAll()
        {
            IList<PointDTO> points = new List<PointDTO>();

            using (var dbContext = new DeliveryServiceDbContext())
            {
                points = dbContext.Points.Select(p => new PointDTO() {
                                             Id = p.Id,
                                             Name = p.Name
                                         }).ToList<PointDTO>();
            }

            return points;
        }

        public void Save(PointDTO point)
        {
            Point pointEntity = typeMapper.Map<PointDTO, Point>(point);

            using (var context = new DeliveryServiceDbContext())
            {
                if (pointEntity.Id == 0)
                {
                    context.Entry(pointEntity).State = EntityState.Added;
                }
                else
                {
                    context.Entry(pointEntity).State = EntityState.Modified;
                }
                
                context.SaveChanges();
            }
        }

        public void Delete(int pointId)
        {
            using (var context = new DeliveryServiceDbContext())
            {
                Point pointEntity = context.Points.Find(pointId);

                context.Entry<Point>(pointEntity).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
    }
}
