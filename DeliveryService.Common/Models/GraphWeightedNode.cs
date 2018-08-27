using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Common.Models
{
    public class GraphWeightedNode
    {
        public int PointId { get; set; }
        public int Cost { get; set; }
        public int Minutes { get; set; }

        public GraphWeightedNode()
        {

        }
    }
}
