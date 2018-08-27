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
    public class RoutesConsumerServiceTest
    {
        private IRoutesRepository routesRepository;
        private IPointsRepository pointsRepository;
        private IRoutesCalculatorService routesCalculatorService;

        private IRoutesConsumerService routesConsumerService;

        [SetUp]
        public void SetupBeforeEachTest()
        {
            this.routesRepository = Substitute.For<IRoutesRepository>();
            this.pointsRepository = Substitute.For<IPointsRepository>();
            this.routesCalculatorService = Substitute.For<IRoutesCalculatorService>();

            this.routesConsumerService = new RoutesConsumerService(this.routesRepository, this.pointsRepository, this.routesCalculatorService);
        }

        [Test]
        public void GetRoutesTest()
        {
            this.routesConsumerService.GetRoutes();

            // assertions
            this.routesRepository.Received().GetRoutes();
        }

        [Test]
        public void GetRouteTest()
        {
            this.routesConsumerService.GetRoute(5);

            // assertions
            this.routesRepository.Received().GetRoute(5);
        }

        [Test]
        public void GetRouteZeroIdTest()
        {
            this.validateExceptionThrownOnGetRouteWithBadArgument(0);
        }

        [Test]
        public void GetRouteNegativeIdTest()
        {
            this.validateExceptionThrownOnGetRouteWithBadArgument(-1);
        }



        private void validateExceptionThrownOnGetRouteWithBadArgument(int routeId)
        {
            var thrownException = Assert.Throws<ArgumentException>(() => this.routesConsumerService.GetRoute(routeId));

            // assertions
            this.routesRepository.DidNotReceive().GetRoute(Arg.Any<int>());
            Assert.That(thrownException.Message, Is.EqualTo("Route Id must be an integer greater than 0."));
        }
    }
}
