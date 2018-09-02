using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Common.Models
{
    /// <summary>
    /// This class represents a path in a graph.
    /// </summary>
    public class GraphPath
    {
        private IList<int> pointIds;
        public IList<int> PointIds {
            get
            {
                return this.pointIds;
            }

            set
            {
                this.pointIds = value;
            }
        }

        public int Cost { get; set; }
        public int Minutes { get; set; }

        public GraphPath()
        {

        }

        public GraphPath(GraphPath path)
        {
            this.pointIds = new List<int>(path.PointIds);
        }

        /// <summary>
        /// Builds a string representation of a graph path.
        /// </summary>
        /// <returns>Returns string in the format { PointId1, PointId2, PointId3, ... }.</returns>
        public override string ToString()
        {
            StringBuilder routeBuilder = new StringBuilder("{");
            int lastIndex = this.pointIds.Count;

            for (int i = 0; i < lastIndex; i++)
            {
                int pointId = this.pointIds[i];
                routeBuilder.Append(pointId.ToString());

                if (i < (lastIndex - 1))
                {
                    routeBuilder.Append(",");
                }

                routeBuilder.Append(" ");
            }

            routeBuilder.Append("}");

            return routeBuilder.ToString();
        }
    }
}
