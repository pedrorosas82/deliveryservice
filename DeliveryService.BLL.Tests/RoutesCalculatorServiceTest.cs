using DeliveryService.Common.DTOs;
using DeliveryService.Common.Interfaces.BLL;
using DeliveryService.Common.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.BLL.Tests
{
    [TestFixture]
    public class RoutesCalculatorServiceTest
    {
        private IRoutesCalculatorService routesCalculatorService;

        [SetUp]
        public void SetupBeforeEachTest()
        {
            this.routesCalculatorService = new RoutesCalculatorService(this.getAllRoutes());
        }

        [Test]
        public void GetAllPathsExampleFromAtoB()
        {
            IEnumerable<GraphPath> resultPaths = this.routesCalculatorService.GetAllNonDirectPaths(1, 2);
            IList<GraphPath> expectedPaths = this.getExpectedPathsFromAtoB();

            // assertions
            Assert.AreEqual(resultPaths.Count(), expectedPaths.Count, "Number of calculated routes does not match.");

            foreach (GraphPath expectedPath in expectedPaths)
            {
                GraphPath resultPath = resultPaths.Where(x => x.PointIds.SequenceEqual(expectedPath.PointIds))
                                                  .Single();

                Assert.IsNotNull(resultPath, String.Format("Missing route {0}", resultPath.ToString()));
                Assert.AreEqual(expectedPath.Cost, resultPath.Cost, String.Format("Route Cost does not match for route {0}", resultPath.ToString()));
                Assert.AreEqual(expectedPath.Minutes, resultPath.Minutes, String.Format("Route Time does not match for route {0}", resultPath.ToString()));
            }
        }

        [Test]
        public void GetAllPathsMinimumLength4()
        {
            IEnumerable<GraphPath> graphPaths = this.routesCalculatorService.GetAllPaths(1, 2, 4);

            // assertions
            Assert.AreEqual(graphPaths.Count(), 4, "Number of calculated routes does not match.");

            var paths = from graphPath in graphPaths
                        select graphPath.PointIds;

            Assert.IsFalse(paths.Any(x => x.SequenceEqual(new List<int>() { 1, 3, 2 })), "Should NOT include route { 1, 3, 2 }");
            Assert.IsTrue(paths.Any(x => x.SequenceEqual(new List<int>() { 1, 5, 4, 6, 7, 2 })), "Missing route { 1, 5, 4, 6, 7, 2 }");
            Assert.IsTrue(paths.Any(x => x.SequenceEqual(new List<int>() { 1, 5, 4, 6, 9, 2 })), "Missing route { 1, 5, 4, 6, 9, 2 }");
            Assert.IsTrue(paths.Any(x => x.SequenceEqual(new List<int>() { 1, 8, 5, 4, 6, 7, 2 })), "Missing route { 1, 8, 5, 4, 6, 7, 2 }");
            Assert.IsTrue(paths.Any(x => x.SequenceEqual(new List<int>() { 1, 8, 5, 4, 6, 9, 2 })), "Missing route { 1, 8, 5, 4, 6, 9, 2 }");
        }

        [Test]
        public void GetAllPathsMinimumLength7()
        {
            IEnumerable<GraphPath> graphPaths = this.routesCalculatorService.GetAllPaths(1, 2, 7);

            // assertions
            Assert.AreEqual(graphPaths.Count(), 2, "Number of calculated routes does not match.");

            var paths = from graphPath in graphPaths
                        select graphPath.PointIds;

            Assert.IsFalse(paths.Any(x => x.SequenceEqual(new List<int>() { 1, 3, 2 })), "Should NOT include route { 1, 3, 2 }");
            Assert.IsFalse(paths.Any(x => x.SequenceEqual(new List<int>() { 1, 5, 4, 6, 7, 2 })), "Should NOT include route { 1, 5, 4, 6, 7, 2 }");
            Assert.IsFalse(paths.Any(x => x.SequenceEqual(new List<int>() { 1, 5, 4, 6, 9, 2 })), "Should NOT include route { 1, 5, 4, 6, 9, 2 }");
            Assert.IsTrue(paths.Any(x => x.SequenceEqual(new List<int>() { 1, 8, 5, 4, 6, 7, 2 })), "Missing route { 1, 8, 5, 4, 6, 7, 2 }");
            Assert.IsTrue(paths.Any(x => x.SequenceEqual(new List<int>() { 1, 8, 5, 4, 6, 9, 2 })), "Missing route { 1, 8, 5, 4, 6, 9, 2 }");
        }

        [Test]
        public void GetAllPathsNoDirectRoutes()
        {
            IEnumerable<GraphPath> graphPaths = this.routesCalculatorService.GetAllNonDirectPaths(1, 5);

            // assertions
            Assert.AreEqual(graphPaths.Count(), 1, "Number of calculated routes does not match.");

            var paths = from graphPath in graphPaths
                        select graphPath.PointIds;

            Assert.IsFalse(paths.Any(x => x.SequenceEqual(new List<int>() { 1, 5 })), "Should NOT include direct route { 1, 5 }");
            Assert.IsTrue(paths.Any(x => x.SequenceEqual(new List<int>() { 1, 8, 5 })), "Missing route { 1, 8, 5 }");
        }


        private IEnumerable<RouteDTO> getAllRoutes()
        {
            IList<RouteDTO> allRoutes = new List<RouteDTO>();

            allRoutes.Add(new RouteDTO()
            {
                Id = 1,
                OriginId = 1,
                OriginName = "A",
                DestinationId = 3,
                DestinationName = "C",
                Cost = 20,
                Minutes = 1 
            });

            allRoutes.Add(new RouteDTO()
            {
                Id = 2,
                OriginId = 1,
                OriginName = "A",
                DestinationId = 8,
                DestinationName = "H",
                Cost = 1,
                Minutes = 10
            });

            allRoutes.Add(new RouteDTO()
            {
                Id = 1,
                OriginId = 1,
                OriginName = "A",
                DestinationId = 5,
                DestinationName = "E",
                Cost = 5,
                Minutes = 30
            });

            allRoutes.Add(new RouteDTO()
            {
                Id = 1,
                OriginId = 3,
                OriginName = "C",
                DestinationId = 2,
                DestinationName = "B",
                Cost = 12,
                Minutes = 1
            });

            allRoutes.Add(new RouteDTO()
            {
                Id = 1,
                OriginId = 4,
                OriginName = "D",
                DestinationId = 6,
                DestinationName = "F",
                Cost = 50,
                Minutes = 4
            });

            allRoutes.Add(new RouteDTO()
            {
                Id = 1,
                OriginId = 5,
                OriginName = "E",
                DestinationId = 4,
                DestinationName = "D",
                Cost = 5,
                Minutes = 3
            });

            allRoutes.Add(new RouteDTO()
            {
                Id = 1,
                OriginId = 6,
                OriginName = "F",
                DestinationId = 7,
                DestinationName = "G",
                Cost = 50,
                Minutes = 40
            });

            allRoutes.Add(new RouteDTO()
            {
                Id = 1,
                OriginId = 6,
                OriginName = "F",
                DestinationId = 9,
                DestinationName = "I",
                Cost = 50,
                Minutes = 45
            });

            allRoutes.Add(new RouteDTO()
            {
                Id = 1,
                OriginId = 7,
                OriginName = "G",
                DestinationId = 2,
                DestinationName = "B",
                Cost = 73,
                Minutes = 64
            });

            allRoutes.Add(new RouteDTO()
            {
                Id = 1,
                OriginId = 8,
                OriginName = "H",
                DestinationId = 5,
                DestinationName = "E",
                Cost = 1,
                Minutes = 30
            });

            allRoutes.Add(new RouteDTO()
            {
                Id = 1,
                OriginId = 9,
                OriginName = "I",
                DestinationId = 2,
                DestinationName = "B",
                Cost = 5,
                Minutes = 65
            });

            return allRoutes;
        }

        private IList<GraphPath> getExpectedPathsFromAtoB()
        {
            IList<GraphPath> graphPaths = new List<GraphPath>();

            graphPaths.Add(new GraphPath()
            {
                PointIds = new List<int>() { 1, 3, 2 },
                Cost = 32,
                Minutes = 2
            });

            graphPaths.Add(new GraphPath()
            {
                PointIds = new List<int>() { 1, 5, 4, 6, 7, 2 },
                Cost = 183,
                Minutes = 141
            });

            graphPaths.Add(new GraphPath()
            {
                PointIds = new List<int>() { 1, 5, 4, 6, 9, 2 },
                Cost = 115,
                Minutes = 147
            });

            graphPaths.Add(new GraphPath()
            {
                PointIds = new List<int>() { 1, 8, 5, 4, 6, 7, 2 },
                Cost = 180,
                Minutes = 151
            });

            graphPaths.Add(new GraphPath()
            {
                PointIds = new List<int>() { 1, 8, 5, 4, 6, 9, 2 },
                Cost = 112,
                Minutes = 157
            });

            return graphPaths;
        }
    }
}
