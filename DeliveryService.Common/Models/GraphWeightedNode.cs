using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Common.Models
{
    /// <summary>
    /// This class represents a destination point with the cost of getting there.
    /// </summary>
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
