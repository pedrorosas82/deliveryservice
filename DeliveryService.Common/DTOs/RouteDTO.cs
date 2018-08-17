using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Common
{
    public class RouteDTO
    {
        public PointDTO Origin { get; set; }
        public PointDTO Destination { get; set; }
        public int Minutes { get; set; }
        public int Cost { get; set; }

        public RouteDTO()
        {

        }
    }
}
