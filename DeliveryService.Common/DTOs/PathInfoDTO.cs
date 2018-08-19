using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Common.DTOs
{
    public class PathInfoDTO
    {
        public IList<int> PointIds { get; set; }
        public IList<string> PointNames { get; set; }
        public int Cost { get; set; }
        public int Minutes { get; set; }

        public PathInfoDTO()
        {
            
        }
    }
}
