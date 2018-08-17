using DeliveryService.DAL.Entities;
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
    }
}
