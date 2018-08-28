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
    public class PointsConsumerServiceTest
    {
        private IPointsRepository pointsRepository;
        private IPointsConsumerService pointsConsumerService;

        [SetUp]
        public void SetupBeforeEachTest()
        {
            this.pointsRepository = Substitute.For<IPointsRepository>();
            this.pointsConsumerService = new PointsConsumerService(this.pointsRepository);
        }

        [Test]
        public void GetPointsTest()
        {
            this.pointsRepository.ListAll().Returns(this.getMockedPoints());

            IEnumerable<PointDTO> points = this.pointsConsumerService.GetPoints();
            IEnumerable<PointDTO> expectedResult = this.getMockedPoints();

            // assertions
            this.pointsRepository.Received().ListAll();
            CollectionAssert.AreEqual(points, expectedResult);
        }

        [Test]
        public void GetPointTest()
        {
            this.pointsRepository.Get(Arg.Any<int>()).Returns(this.getMockedPoints().ElementAt(0));

            PointDTO point = this.pointsConsumerService.GetPoint(5);
            PointDTO expectedResult = this.getMockedPoints().ElementAt(0);

            // assertions
            this.pointsRepository.Received().Get(5);
            Assert.AreEqual(point, expectedResult);
        }

        [Test]
        public void GetPointTestZeroIdTest()
        {
            this.pointsRepository.Get(Arg.Any<int>()).Returns((PointDTO)null);

            PointDTO point = this.pointsConsumerService.GetPoint(0);
            PointDTO expectedResult = null;

            // assertions
            this.pointsRepository.DidNotReceive().Get(0);
            Assert.AreEqual(point, expectedResult);
        }

        [Test]
        public void GetPointTestNegativeIdTest()
        {
            this.pointsRepository.Get(Arg.Any<int>()).Returns((PointDTO)null);

            PointDTO point = this.pointsConsumerService.GetPoint(-1);
            PointDTO expectedResult = null;

            // assertions
            this.pointsRepository.DidNotReceive().Get(-1);
            Assert.AreEqual(point, expectedResult);
        }


        private IEnumerable<PointDTO> getMockedPoints()
        {
            return new List<PointDTO>()
            {
                new PointDTO()
                {
                    Id = 1,
                    Name = "A"
                },

                new PointDTO()
                {
                    Id = 2,
                    Name = "B"
                },

                new PointDTO()
                {
                    Id = 3,
                    Name = "C"
                },

                new PointDTO()
                {
                    Id = 4,
                    Name = "D"
                },

                new PointDTO()
                {
                    Id = 5,
                    Name = "E"
                },

                new PointDTO()
                {
                    Id = 6,
                    Name = "F"
                },

                new PointDTO()
                {
                    Id = 7,
                    Name = "G"
                },

                new PointDTO()
                {
                    Id = 8,
                    Name = "H"
                },

                new PointDTO()
                {
                    Id = 9,
                    Name = "I"
                }
            };
        }
    }
}
