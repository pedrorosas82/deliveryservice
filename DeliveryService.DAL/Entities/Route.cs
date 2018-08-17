using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.DAL.Entities
{
    public partial class Route
    {
        public int Id { get; set; }

        public Point Origin { get; set; }
        public Point Destination { get; set; }
        public int Cost { get; set; }
        public int Minutes { get; set; }
    }
}
