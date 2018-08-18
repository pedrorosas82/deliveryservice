using AutoMapper;
using DeliveryService.Common;
using DeliveryService.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeliveryService.WebApi.App_Start
{
    public class AutoMapperConfig
    {
        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(x => {
                x.CreateMap<PointDTO, Point>();
                x.CreateMap<RouteDTO, Route>();
            });

            return config.CreateMapper();
        }
    }
}