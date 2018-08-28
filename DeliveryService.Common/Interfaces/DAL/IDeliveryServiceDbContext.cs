using DeliveryService.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Common.Interfaces.DAL
{
    public interface IDeliveryServiceDbContext
    {
        DbSet<Point> Points { get; set; }
        DbSet<Route> Routes { get; set; }
    }
}
