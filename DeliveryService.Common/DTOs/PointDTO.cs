using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Common.DTOs
{
    public class PointDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public PointDTO()
        {

        }

        public override bool Equals(object obj)
        {
            bool isEqual = true;

            PointDTO point = obj as PointDTO;

            if (point != null)
            {
                isEqual = isEqual && point.Id.Equals(this.Id);
                isEqual = isEqual && String.Equals(point.Name, this.Name);
            }
            else
            {
                isEqual = false;
            }

            return isEqual;
        }
    }
}
