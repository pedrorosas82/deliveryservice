﻿using DeliveryService.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.BLL.Interfaces
{
    public interface IPointsConsumerService
    {
        IEnumerable<PointDTO> GetPoints();
        PointDTO GetPoint(int pointId);
    }
}
