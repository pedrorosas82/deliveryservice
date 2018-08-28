using DeliveryService.Common.DTOs;
using DeliveryService.Common.Interfaces.BLL;
using DeliveryService.Common.Interfaces.DAL;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.BLL.Tests
{
    [TestFixture]
    public class RoutesAdminServiceTest
    {
        private IRoutesAdminService routesAdminService;
        private IRoutesRepository routesRepository;

        [SetUp]
        public void SetupBeforeEachTest()
        {
            this.routesRepository = Substitute.For<IRoutesRepository>();
            this.routesAdminService = new RoutesAdminService(this.routesRepository);
        }

        [Test]
        public void CreateRouteTest()
        {
            RouteDTO route = new RouteDTO()
            {
                OriginId = 1,
                DestinationId = 2,
                Cost = 5,
                Minutes = 30
            };

            this.routesAdminService.CreateRoute(route);

            // assertions
            this.routesRepository.Received().Save(route);
        }

        [Test]
        public void CreateRouteWithIdTest()
        {
            RouteDTO route = new RouteDTO()
            {
                Id = 5,
                OriginId = 1,
                DestinationId = 2,
                Cost = 5,
                Minutes = 30
            };

            var thrownException = Assert.Throws<ArgumentException>(() => this.routesAdminService.CreateRoute(route));

            // assertions
            this.routesRepository.DidNotReceive().Save(Arg.Any<RouteDTO>());
            Assert.That(thrownException.Message, Is.EqualTo("Route Id is assigned automatically. Cannot create route with the Id assigned."));
        }

        [Test]
        public void UpdateRouteTest()
        {
            RouteDTO route = new RouteDTO()
            {
                Id = 5,
                OriginId = 1,
                DestinationId = 2,
                Cost = 5,
                Minutes = 30
            };

            this.routesAdminService.UpdateRoute(route);

            // assertions
            this.routesRepository.Received().Save(route);
        }

        [Test]
        public void UpdatePointZeroIdTest()
        {
            RouteDTO route = new RouteDTO()
            {
                Id = 0,
                OriginId = 1,
                DestinationId = 2,
                Cost = 5,
                Minutes = 30
            };

            this.validateExceptionThrownOnUpdateWithBadArgument(route);
        }

        [Test]
        public void UpdatePointNegativeIdTest()
        {
            RouteDTO route = new RouteDTO()
            {
                Id = -1,
                OriginId = 1,
                DestinationId = 2,
                Cost = 5,
                Minutes = 30
            };

            this.validateExceptionThrownOnUpdateWithBadArgument(route);
        }

        [Test]
        public void DeleteRouteTest()
        {
            this.routesAdminService.DeleteRoute(5);

            // assertions
            this.routesRepository.Received().Delete(5);
        }

        [Test]
        public void DeletePointZeroIdArgumentTest()
        {
            this.validateExceptionThrownOnDeleteWithBadArgument(0);
        }



        private void validateExceptionThrownOnDeleteWithBadArgument(int routeId)
        {
            var thrownException = Assert.Throws<ArgumentException>(() => this.routesAdminService.DeleteRoute(routeId));

            // assertions
            this.routesRepository.DidNotReceive().Delete(Arg.Any<int>());
            Assert.That(thrownException.Message, Is.EqualTo("Route Id must be an integer greater than 0."));
        }

        private void validateExceptionThrownOnUpdateWithBadArgument(RouteDTO route)
        {
            var thrownException = Assert.Throws<ArgumentException>(() => this.routesAdminService.UpdateRoute(route));

            // assertions
            this.routesRepository.DidNotReceive().Save(Arg.Any<RouteDTO>());
            Assert.That(thrownException.Message, Is.EqualTo("Route Id must be an integer greater than 0."));
        }
    }
}
