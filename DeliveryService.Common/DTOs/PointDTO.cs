using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Common.DTOs
{
    /// <summary>
    /// This class represents a point.
    /// </summary>
    public class PointDTO
    {
        public int Id { get; set; }

        [MaxLength(30, ErrorMessage ="The max length for the Point Name is 30 characters")]
        public string Name { get; set; }

        public PointDTO()
        {

        }

        public override bool Equals(object obj)
        {
            bool isEqual = false;

            PointDTO point = obj as PointDTO;

            if (point != null)
            {
                isEqual = point.Id.Equals(this.Id) && 
                          String.Equals(point.Name, this.Name);
            }

            return isEqual;
        }
    }
}
