using DeliveryService.DAL;
using DeliveryService.WebApi.App_Start;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace DeliveryService.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer<DeliveryServiceDbContext>(new DropCreateDatabaseIfModelChanges<DeliveryServiceDbContext>());
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
