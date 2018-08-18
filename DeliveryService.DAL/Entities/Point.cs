using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.DAL.Entities
{
    public class Point
    {
        public int Id { get; set; }

        public String Name { get; set; }

        public IList<Route> RoutesOrigin { get; set; }
        public IList<Route> RoutesDestination { get; set; }
    }
}
