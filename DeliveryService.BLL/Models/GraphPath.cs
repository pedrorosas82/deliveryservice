using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.BLL.Models
{
    public class GraphPath
    {
        public IList<int> PointIds { get; set; }
        public int Cost { get; set; }
        public int Minutes { get; set; }

        public GraphPath()
        {

        }

        public GraphPath(GraphPath path)
        {
            this.PointIds = new List<int>(path.PointIds);
        }
    }
}
