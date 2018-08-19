using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Common.DTOs
{
    public class PathDTO
    {
        public IList<int> PointIds { get; set; }
        public IList<string> PointNames { get; set; }
        public int TotalCost { get; set; }
        public int TotalMinutes { get; set; }

        public PathDTO()
        {
            
        }

        public PathDTO(PathDTO path)
        {
            this.PointIds = new List<int>(path.PointIds);
            /*this.PointNames = new List<string>(path.PointNames);
            this.TotalCost = path.TotalCost;
            this.TotalMinutes = path.TotalMinutes;*/
        }
    }
}
