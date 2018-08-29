using DeliveryService.Common.DTOs;
using DeliveryService.Common.Interfaces.BLL;
using DeliveryService.Common.Interfaces.DAL;
using DeliveryService.WebApi.Controllers;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace DeliveryService.WebApi.Tests
{
    [TestFixture]
    public class RoutesControllerTest
    {
        private IRoutesConsumerService routesConsumerService;
        private IRoutesAdminService routesAdminService;
        private RoutesController routesController; 

        [SetUp]
        public void SetupBeforeEachTest()
        {
            this.routesConsumerService = Substitute.For<IRoutesConsumerService>();
            this.routesAdminService = Substitute.For<IRoutesAdminService>();
            this.routesController = new RoutesController(this.routesConsumerService, this.routesAdminService);
        }

        [Test]
        public void GetAllRoutesTest()
        {
            this.routesConsumerService.GetRoutes().Returns(this.getMockedRoutes());

            IEnumerable<RouteDTO> resultRoutes = this.routesController.Get();
            IEnumerable<RouteDTO> expectedResult = this.getMockedRoutes();

            // assertions
            this.routesConsumerService.Received().GetRoutes();
            CollectionAssert.AreEquivalent(resultRoutes, expectedResult);
        }

        [Test]
        public void GetAllRoutesEmptyListTest()
        {
            this.routesConsumerService.GetRoutes().Returns(new List<RouteDTO>());

            IEnumerable<RouteDTO> resultRoutes = this.routesController.Get();
            IList<RouteDTO> expectedResult = new List<RouteDTO>();

            // assertions
            this.routesConsumerService.Received().GetRoutes();
            CollectionAssert.AreEquivalent(resultRoutes, expectedResult);
        }

        [Test]
        public void GetAllRoutesNullListTest()
        {
            this.routesConsumerService.GetRoutes().Returns((IEnumerable<RouteDTO>) null);

            IEnumerable<RouteDTO> resultRoutes = this.routesController.Get();
            IEnumerable<RouteDTO> expectedResult = new List<RouteDTO>();

            // assertions
            this.routesConsumerService.Received().GetRoutes();
            CollectionAssert.AreEquivalent(resultRoutes, expectedResult);
        }

        [Test]
        public void GetRouteTest()
        {
            this.routesConsumerService.GetRoute(Arg.Any<int>()).Returns(this.getMockedRoutes().ElementAt(0));

            RouteDTO resultRoute = this.routesController.Get(10);
            RouteDTO expectedResult = this.getMockedRoutes().ElementAt(0);

            // assertions
            this.routesConsumerService.Received().GetRoute(10);
            Assert.AreEqual(resultRoute, expectedResult);
        }

        [Test]
        public void GetNonExistingRouteTest()
        {
            this.routesConsumerService.GetRoute(Arg.Any<int>()).Returns((RouteDTO)null);
            var thrownException = Assert.Throws<HttpResponseException>(() => this.routesController.Get(10));

            // assertions
            this.routesConsumerService.Received().GetRoute(Arg.Any<int>());
            Assert.AreEqual(thrownException.Response.StatusCode, HttpStatusCode.BadRequest);
        }

        [Test]
        public void PostRouteTest()
        {
            RouteDTO mockedRoute = this.getMockedRoutes().ElementAt(0);
            this.routesAdminService.CreateRoute(Arg.Any<RouteDTO>()).Returns(mockedRoute);

            RouteDTO route = new RouteDTO()
            {
                OriginId = 1,
                OriginName = "A",
                DestinationId = 2,
                DestinationName = "B",
                Cost = 10,
                Minutes = 2
            };

            IHttpActionResult actionResult = this.routesController.Post(route);
            RouteDTO expectedResult = mockedRoute;

            // assertions
            this.routesAdminService.Received().CreateRoute(route);
            OkNegotiatedContentResult<RouteDTO> contentResult = actionResult as OkNegotiatedContentResult<RouteDTO>;

            Assert.IsNotNull(contentResult);
            Assert.AreEqual(expectedResult, contentResult.Content);
        }

        [Test]
        public void PostInvalidRouteTest()
        {
            RouteDTO mockedRoute = this.getMockedRoutes().ElementAt(0);
            this.routesAdminService.UpdateRoute(Arg.Any<RouteDTO>()).Returns(mockedRoute);

            RouteDTO route = new RouteDTO()
            {
                Id = 1,
                OriginId = 1,
                OriginName = "A",
                DestinationId = 2,
                DestinationName = "B",
                Cost = 10,
                Minutes = 2
            };

            IHttpActionResult actionResult = this.routesController.Post(route);

            // assertions
            this.routesAdminService.DidNotReceive().CreateRoute(route);
            BadRequestErrorMessageResult badRequestMessage = actionResult as BadRequestErrorMessageResult;

            Assert.IsNotNull(badRequestMessage);
            Assert.AreEqual(badRequestMessage.Message, "Route Id cannot be defined for new entity.");
        }

        [Test]
        public void PostInvalidRouteStateTest()
        {
            RouteDTO mockedRoute = this.getMockedRoutes().ElementAt(0);
            this.routesAdminService.CreateRoute(Arg.Any<RouteDTO>()).Returns(mockedRoute);
            this.routesController.ModelState.AddModelError("Name", "Error Message.");

            RouteDTO route = new RouteDTO()
            {
                Cost = 10
            };

            IHttpActionResult actionResult = this.routesController.Post(route);

            // assertions
            this.routesAdminService.DidNotReceive().CreateRoute(route);

            InvalidModelStateResult invalidModelStateResult = actionResult as InvalidModelStateResult;
            Assert.IsNotNull(invalidModelStateResult);
        }

        [Test]
        public void PutRouteTest()
        {
            RouteDTO mockedRoute = this.getMockedRoutes().ElementAt(0);
            this.routesAdminService.UpdateRoute(Arg.Any<RouteDTO>()).Returns(mockedRoute);

            RouteDTO route = new RouteDTO()
            {
                Id = 3,
                OriginId = 1,
                OriginName = "A",
                DestinationId = 2,
                DestinationName = "B",
                Cost = 10,
                Minutes = 2
            };

            IHttpActionResult actionResult = this.routesController.Put(route);
            RouteDTO expectedResult = mockedRoute;

            // assertions
            this.routesAdminService.Received().UpdateRoute(route);
            OkNegotiatedContentResult<RouteDTO> contentResult = actionResult as OkNegotiatedContentResult<RouteDTO>;

            Assert.IsNotNull(contentResult);
            Assert.AreEqual(expectedResult, contentResult.Content);
        }

        [Test]
        public void PutInvalidRouteTest()
        {
            RouteDTO mockedRoute = this.getMockedRoutes().ElementAt(0);
            this.routesAdminService.UpdateRoute(Arg.Any<RouteDTO>()).Returns(mockedRoute);

            RouteDTO route = new RouteDTO()
            {
                Id = 0,
                OriginId = 1,
                OriginName = "A",
                DestinationId = 2,
                DestinationName = "B",
                Cost = 10,
                Minutes = 2
            };

            IHttpActionResult actionResult = this.routesController.Put(route);

            // assertions
            this.routesAdminService.DidNotReceive().UpdateRoute(route);
            BadRequestErrorMessageResult badRequestMessage = actionResult as BadRequestErrorMessageResult;

            Assert.IsNotNull(badRequestMessage);
            Assert.AreEqual(badRequestMessage.Message, "Route Id must be greater than 0.");
        }

        [Test]
        public void PutInvalidRouteStateTest()
        {
            RouteDTO mockedRoute = this.getMockedRoutes().ElementAt(0);
            this.routesAdminService.UpdateRoute(Arg.Any<RouteDTO>()).Returns(mockedRoute);
            this.routesController.ModelState.AddModelError("Name", "Error Message.");

            RouteDTO route = new RouteDTO()
            {
                Id = 1,
                Cost = 10
            };

            IHttpActionResult actionResult = this.routesController.Put(route);

            // assertions
            this.routesAdminService.DidNotReceive().UpdateRoute(route);

            InvalidModelStateResult invalidModelStateResult = actionResult as InvalidModelStateResult;
            Assert.IsNotNull(invalidModelStateResult);
        }

        [Test]
        public void DeleteRouteTest()
        {
            this.routesAdminService.When(x => x.DeleteRoute(Arg.Any<int>()))
                                   .Do(x => { return; });

            this.routesController.Delete(10);

            // assertions
            this.routesAdminService.Received().DeleteRoute(10);
        }

        [Test]
        public void DeleteRouteInvalidIdTest()
        {
            this.routesAdminService.When(x => x.DeleteRoute(Arg.Any<int>()))
                                   .Do(x => { return; });

            IHttpActionResult actionResult = this.routesController.Delete(-1);

            // assertions
            BadRequestErrorMessageResult badRequestMessage = actionResult as BadRequestErrorMessageResult;
            Assert.IsNotNull(badRequestMessage);
            Assert.AreEqual(badRequestMessage.Message, "Route Id must be greater than 0.");
        }

        [Test]
        public void GetNonDirectRoutesTest()
        {
            this.routesConsumerService.GetPaths(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>()).Returns(this.getMockedPaths());

            IEnumerable<PathInfoDTO> resultPaths = this.routesController.GetNonDirectPaths(1, 2);
            IEnumerable<PathInfoDTO> expectedResult = this.getMockedPaths();

            // assertions
            this.routesConsumerService.Received().GetPaths(1, 2, 3);
            CollectionAssert.AreEquivalent(resultPaths, expectedResult);
        }

        [Test]
        public void GetNonDirectRoutesEmptyListTest()
        {
            this.routesConsumerService.GetPaths(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>()).Returns(new List<PathInfoDTO>());

            IEnumerable<PathInfoDTO> resultPaths = this.routesController.GetNonDirectPaths(1, 2);
            IEnumerable<PathInfoDTO> expectedResult = new List<PathInfoDTO>();

            // assertions
            this.routesConsumerService.Received().GetPaths(1, 2, 3);
            CollectionAssert.AreEquivalent(resultPaths, expectedResult);
        }

        [Test]
        public void GetNonDirectRoutesNullListTest()
        {
            this.routesConsumerService.GetPaths(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>()).Returns((IEnumerable<PathInfoDTO>)null);

            IEnumerable<PathInfoDTO> resultPaths = this.routesController.GetNonDirectPaths(1, 2);
            IEnumerable<PathInfoDTO> expectedResult = new List<PathInfoDTO>();

            // assertions
            this.routesConsumerService.Received().GetPaths(1, 2, 3);
            CollectionAssert.AreEquivalent(resultPaths, expectedResult);
        }


        private IEnumerable<RouteDTO> getMockedRoutes()
        {
            return new List<RouteDTO>()
            {
                new RouteDTO()
                {
                    Id = 1,
                    OriginId = 1,
                    OriginName = "A",
                    DestinationId = 2,
                    DestinationName = "B",
                    Cost = 10,
                    Minutes = 30
                },

                new RouteDTO()
                {
                    Id = 2,
                    OriginId = 2,
                    OriginName = "B",
                    DestinationId = 3,
                    DestinationName = "C",
                    Cost = 5,
                    Minutes = 15
                },

                new RouteDTO()
                {
                    Id = 3,
                    OriginId = 1,
                    OriginName = "A",
                    DestinationId = 3,
                    DestinationName = "C",
                    Cost = 40,
                    Minutes = 50
                }
            };
        }

        private IEnumerable<PathInfoDTO> getMockedPaths()
        {
            return new List<PathInfoDTO>()
            {
                new PathInfoDTO()
                {
                    PointIds = new List<int>() { 1, 3, 2 },
                    Cost = 10,
                    Minutes = 30,
                    PointNames = new List<string>() { "A", "C", "B" }
                },

                new PathInfoDTO()
                {
                    PointIds = new List<int>() { 1, 5, 4, 2 },
                    Cost = 5,
                    Minutes = 20,
                    PointNames = new List<string>() { "A", "E", "D", "B" }
                },

                new PathInfoDTO()
                {
                    PointIds = new List<int>() { 1, 8, 6, 2 },
                    Cost = 25,
                    Minutes = 65,
                    PointNames = new List<string>() { "A", "H", "F", "B" }
                }
            };
        }

    }
}
