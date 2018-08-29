using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Common.DTOs
{
    public class RouteDTO
    {
        public int Id { get; set; }

        [Required]
        public int OriginId { get; set; }
        public string OriginName { get; set; }

        [Required]
        public int DestinationId { get; set; }
        public string DestinationName { get; set; }

        public int Minutes { get; set; }
        public int Cost { get; set; }

        public RouteDTO()
        {

        }

        public override bool Equals(object obj)
        {
            bool isEqual = true;

            RouteDTO route = obj as RouteDTO;

            if (route != null)
            {
                isEqual = isEqual && route.Id.Equals(this.Id);
                isEqual = isEqual && route.OriginId.Equals(this.OriginId);
                isEqual = isEqual && String.Equals(route.OriginName, this.OriginName);
                isEqual = isEqual && route.DestinationId.Equals(this.DestinationId);
                isEqual = isEqual && String.Equals(route.DestinationName, this.DestinationName);
                isEqual = isEqual && route.Minutes.Equals(this.Minutes);
                isEqual = isEqual && route.Cost.Equals(this.Cost);
            }
            else
            {
                isEqual = false;
            }

            return isEqual;
        }
    }
}
