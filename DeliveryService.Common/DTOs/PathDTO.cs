﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Common.DTOs
{
    public class PathDTO
    {
        public IList<int> PointIds { get; set; }

        public PathDTO()
        {
            
        }

        public PathDTO(PathDTO path)
        {
            this.PointIds = new List<int>(path.PointIds);
        }
    }
}
