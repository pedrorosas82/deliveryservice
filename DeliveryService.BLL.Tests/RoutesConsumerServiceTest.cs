using DeliveryService.Common.DTOs;
using DeliveryService.Common.Interfaces.BLL;
using DeliveryService.Common.Interfaces.DAL;
using DeliveryService.Common.Models;
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
            this.routesRepository.Received().ListAll();


            this.routesRepository.ListAll().Returns(this.getMockedRoutes());

            IEnumerable<RouteDTO> routes = this.routesRepository.ListAll();
            IEnumerable<RouteDTO> expectedResult = this.getMockedRoutes();

            // assertions
            this.routesRepository.Received().ListAll();
            CollectionAssert.AreEqual(routes, expectedResult);
        }

        [Test]
        public void GetRouteTest()
        {
            this.routesRepository.Get(Arg.Any<int>()).Returns(this.getMockedRoutes().ElementAt(0));

            RouteDTO route = this.routesConsumerService.GetRoute(5);
            RouteDTO expectedResult = this.getMockedRoutes().ElementAt(0);

            // assertions
            this.routesRepository.Received().Get(5);
            Assert.AreEqual(route, expectedResult);
        }

        [Test]
        public void GetRouteZeroIdTest()
        {
            this.routesRepository.Get(Arg.Any<int>()).Returns((RouteDTO)null);

            RouteDTO route = this.routesConsumerService.GetRoute(0);
            RouteDTO expectedResult = null;

            // assertions
            this.routesRepository.DidNotReceive().Get(0);
            Assert.AreEqual(route, expectedResult);
        }

        [Test]
        public void GetRouteNegativeIdTest()
        {
            this.routesRepository.Get(Arg.Any<int>()).Returns((RouteDTO)null);

            RouteDTO route = this.routesRepository.Get(-1);
            RouteDTO expectedResult = null;

            // assertions
            this.pointsRepository.DidNotReceive().Get(-1);
            Assert.AreEqual(route, expectedResult);
        }

        [Test]
        public void GetPathsNonExistingOriginTest()
        {
            this.pointsRepository.ListAll()
                                 .Returns(this.getAllPoints());

            var thrownException = Assert.Throws<ArgumentException>(() => this.routesConsumerService.GetPaths(20, 4, 3));

            // assertions
            this.routesCalculatorService.DidNotReceive().GetAllPaths(Arg.Any<int>(), Arg.Any<int>());
            Assert.That(thrownException.Message, Is.EqualTo("Origin Id 20 does not exist."));
        }

        [Test]
        public void GetPathsNonExistingDestinationTest()
        {
            this.pointsRepository.ListAll()
                                 .Returns(this.getAllPoints());

            var thrownException = Assert.Throws<ArgumentException>(() => this.routesConsumerService.GetPaths(1, 20, 3));

            // assertions
            this.routesCalculatorService.DidNotReceive().GetAllPaths(Arg.Any<int>(), Arg.Any<int>());
            Assert.That(thrownException.Message, Is.EqualTo("Destination Id 20 does not exist."));
        }

        [Test]
        public void GetPathsFromAtoBTest()
        {
            // mock getPaths
            this.routesCalculatorService.GetAllPaths(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>())
                                        .Returns(this.getCalculatedRoutesFromAtoB());

            this.pointsRepository.ListAll()
                                 .Returns(this.getAllPoints());
                                        

            IEnumerable<PathInfoDTO> resultPaths = this.routesConsumerService.GetPaths(1, 2, 3);
            IEnumerable<PathInfoDTO> expectedPaths = this.getExpectedPathsFromAtoB();

            // assertions
            Assert.AreEqual(resultPaths.Count(), expectedPaths.Count(), "The number of paths does not match.");
            
            foreach (PathInfoDTO resultPath in resultPaths)
            {
                PathInfoDTO expectedPath = expectedPaths.Where(x => x.PointIds.SequenceEqual(resultPath.PointIds)).Single();

                CollectionAssert.AreEqual(resultPath.PointNames, expectedPath.PointNames);
                Assert.AreEqual(resultPath.Cost, expectedPath.Cost);
                Assert.AreEqual(resultPath.Minutes, expectedPath.Minutes);
            }
        }

        [Test]
        public void GetPathsEmptyGraphPathsTest()
        {
            // mock getPaths
            this.routesCalculatorService.GetAllPaths(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>())
                                        .Returns(new List<GraphPath>() { });

            this.pointsRepository.ListAll()
                                 .Returns(this.getAllPoints());


            IEnumerable<PathInfoDTO> resultPaths = this.routesConsumerService.GetPaths(1, 2, 3);
            IEnumerable<PathInfoDTO> expectedPaths = new List<PathInfoDTO>();

            // assertions
            CollectionAssert.AreEquivalent(resultPaths, expectedPaths);
        }

        [Test]
        public void GetPathsNullGraphPathsTest()
        {
            // mock getPaths
            this.routesCalculatorService.GetAllPaths(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>())
                                        .Returns((IEnumerable<GraphPath>) null);

            this.pointsRepository.ListAll()
                                 .Returns(this.getAllPoints());


            IEnumerable<PathInfoDTO> resultPaths = this.routesConsumerService.GetPaths(1, 2, 3);
            IEnumerable<PathInfoDTO> expectedPaths = new List<PathInfoDTO>();

            // assertions
            CollectionAssert.AreEquivalent(resultPaths, expectedPaths);
        }

        [Test]
        public void GetPathsEmptyPointsTest()
        {
            // mock getPaths
            this.routesCalculatorService.GetAllPaths(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>())
                                        .Returns(this.getCalculatedRoutesFromAtoB());

            this.pointsRepository.ListAll()
                                 .Returns(new List<PointDTO>());


            var thrownException = Assert.Throws<ArgumentException>(() => this.routesConsumerService.GetPaths(1, 2, 3));

            // assertions
            this.routesCalculatorService.DidNotReceive().GetAllPaths(Arg.Any<int>(), Arg.Any<int>());
            Assert.That(thrownException.Message, Is.EqualTo("Origin Id 1 does not exist."));
        }

        [Test]
        public void GetPathsNullPointsTest()
        {
            // mock getPaths
            this.routesCalculatorService.GetAllPaths(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>())
                                        .Returns(this.getCalculatedRoutesFromAtoB());

            this.pointsRepository.ListAll()
                                 .Returns((IEnumerable<PointDTO>)null);


            var thrownException = Assert.Throws<ArgumentException>(() => this.routesConsumerService.GetPaths(1, 2, 3));

            // assertions
            this.routesCalculatorService.DidNotReceive().GetAllPaths(Arg.Any<int>(), Arg.Any<int>());
            Assert.That(thrownException.Message, Is.EqualTo("Origin Id 1 does not exist."));
        }


        private IEnumerable<PointDTO> getAllPoints()
        {
            IList<PointDTO> points = new List<PointDTO>();

            points.Add(new PointDTO()
            {
                Id = 1,
                Name = "A"
            });

            points.Add(new PointDTO()
            {
                Id = 2,
                Name = "B"
            });

            points.Add(new PointDTO()
            {
                Id = 3,
                Name = "C"
            });

            points.Add(new PointDTO()
            {
                Id = 4,
                Name = "D"
            });

            points.Add(new PointDTO()
            {
                Id = 5,
                Name = "E"
            });

            points.Add(new PointDTO()
            {
                Id = 6,
                Name = "F"
            });

            points.Add(new PointDTO()
            {
                Id = 7,
                Name = "G"
            });

            points.Add(new PointDTO()
            {
                Id = 8,
                Name = "H"
            });

            points.Add(new PointDTO()
            {
                Id = 9,
                Name = "I"
            });

            return points;
        }

        private IEnumerable<PathInfoDTO> getExpectedPathsFromAtoB()
        {
            IList<PathInfoDTO> paths = new List<PathInfoDTO>();

            paths.Add(new PathInfoDTO()
            {
                PointIds = new List<int>() { 1, 3, 2 },
                PointNames = new List<string>() { "A", "C", "B" },
                Cost = 5,
                Minutes = 10
            });

            paths.Add(new PathInfoDTO()
            {
                PointIds = new List<int>() { 1, 4, 5, 2 },
                PointNames = new List<string>() { "A", "D", "E", "B" },
                Cost = 45,
                Minutes = 34
            });

            paths.Add(new PathInfoDTO()
            {
                PointIds = new List<int>() { 1, 9, 4, 5, 2 },
                PointNames = new List<string>() { "A", "I", "D", "E", "B" },
                Cost = 35,
                Minutes = 28
            });

            return paths;
        }

        private IEnumerable<GraphPath> getCalculatedRoutesFromAtoB()
        {
            IList<GraphPath> graphPaths = new List<GraphPath>();

            graphPaths.Add(new GraphPath()
            {
                PointIds = new List<int>() { 1, 3, 2 },
                Cost = 5,
                Minutes = 10
            });

            graphPaths.Add(new GraphPath()
            {
                PointIds = new List<int>() { 1, 4, 5, 2 },
                Cost = 45,
                Minutes = 34
            });

            graphPaths.Add(new GraphPath()
            {
                PointIds = new List<int>() { 1, 9, 4, 5, 2 },
                Cost = 35,
                Minutes = 28
            });

            return graphPaths;
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
                    Cost = 2,
                    Minutes = 10
                },

                new RouteDTO()
                {
                    Id = 2,
                    OriginId = 2,
                    OriginName = "B",
                    DestinationId = 3,
                    DestinationName = "C",
                    Cost = 1,
                    Minutes = 40
                },

                new RouteDTO()
                {
                    Id = 3,
                    OriginId = 1,
                    OriginName = "A",
                    DestinationId = 3,
                    DestinationName = "C",
                    Cost = 1,
                    Minutes = 20
                }
            };
        }
    }
}
