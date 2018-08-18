using DeliveryService.DAL.Entities;
using DeliveryService.DAL.EntityConfigurations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.DAL
{
    public class DeliveryServiceDbContext : DbContext
    {
        public DbSet<Point> Points { get; set; }
        public DbSet<Route> Routes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new RoutesEntityConfig());
        }
    }
}
