using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Common.DTOs
{
    /// <summary>
    /// This class represents a path in the graph.
    /// </summary>
    public class PathInfoDTO
    {
        public IList<int> PointIds { get; set; }
        public IList<string> PointNames { get; set; }
        public int Cost { get; set; }
        public int Minutes { get; set; }

        public PathInfoDTO()
        {
            
        }

        public override bool Equals(object obj)
        {
            bool isEqual = false;

            PathInfoDTO path = obj as PathInfoDTO;

            if (path != null)
            {
                isEqual = path.PointIds.SequenceEqual(this.PointIds) && 
                          path.PointNames.SequenceEqual(this.PointNames) &&
                          path.Cost.Equals(this.Cost) &&
                          path.Minutes.Equals(this.Minutes);
            }

            return isEqual;
        }
    }
}
