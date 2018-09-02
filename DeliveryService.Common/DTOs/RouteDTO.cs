using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DeliveryService.Common.DTOs
{
    /// <summary>
    /// This class represents a route.
    /// </summary>
    public class RouteDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The route origin point is not defined.")]
        public int OriginId { get; set; }
        public string OriginName { get; set; }

        [Required(ErrorMessage = "The route destination point is not defined.")]
        public int DestinationId { get; set; }
        public string DestinationName { get; set; }

        public int Minutes { get; set; }
        public int Cost { get; set; }

        public RouteDTO()
        {

        }

        public override bool Equals(object obj)
        {
            bool isEqual = false;

            RouteDTO route = obj as RouteDTO;

            if (route != null)
            {
                isEqual = route.Id.Equals(this.Id) && 
                          route.OriginId.Equals(this.OriginId) && 
                          String.Equals(route.OriginName, this.OriginName) && 
                          route.DestinationId.Equals(this.DestinationId) && 
                          String.Equals(route.DestinationName, this.DestinationName) && 
                          route.Minutes.Equals(this.Minutes) && 
                          route.Cost.Equals(this.Cost);
            }

            return isEqual;
        }
    }
}
