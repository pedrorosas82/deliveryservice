using AutoMapper;
using DeliveryService.BLL;
using DeliveryService.Common.Interfaces.BLL;
using DeliveryService.Common.Interfaces.DAL;
using DeliveryService.Neo4j.DAL.Repositories;
using DeliveryService.Identity.DAL.Repositories;
using DeliveryService.WebApi.App_Start;
using Neo4j.Driver.V1;
using System;
using System.Configuration;
using Unity;

namespace DeliveryService.WebApi
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              RegisterInstances(container);

              return container;
          });

        private static void RegisterInstances(UnityContainer container)
        {
            container.RegisterInstance<IMapper>(AutoMapperConfig.GetMapper());
            registerNeo4jDriver(container);
        }

        private static void registerNeo4jDriver(UnityContainer container)
        {
            var url = ConfigurationManager.AppSettings["GraphDBUrl"];
            var driver = GraphDatabase.Driver(url);

            container.RegisterInstance<IDriver>(driver);
        }

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // Register your type's mappings here.

            // services - BLL
            container.RegisterType<IPointsAdminService, PointsAdminService>();
            container.RegisterType<IPointsConsumerService, PointsConsumerService>();
            container.RegisterType<IRoutesAdminService, RoutesAdminService>();
            container.RegisterType<IRoutesConsumerService, RoutesConsumerService>();
            container.RegisterType<IRoutesCalculatorService, RoutesCalculatorService>();
            container.RegisterType<IAuthenticationService, AuthenticationService>();

            // repositories - DAL
            container.RegisterType<IPointsRepository, PointsRepository>();
            container.RegisterType<IRoutesRepository, RoutesRepository>();
            container.RegisterType<IAuthenticationRepository, AuthenticationRepository>();
        }
    }
}