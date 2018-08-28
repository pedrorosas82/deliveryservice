using DeliveryService.Common.DTOs;
using DeliveryService.Common.Interfaces.BLL;
using DeliveryService.Common.Interfaces.DAL;
using DeliveryService.WebApi.Controllers;
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
    public class PointsControllerTest
    {
        private IPointsConsumerService pointsConsumerService;
        private IPointsAdminService pointsAdminService;
        private PointsController pointsController; 

        [SetUp]
        public void SetupBeforeEachTest()
        {
            this.pointsConsumerService = Substitute.For<IPointsConsumerService>();
            this.pointsAdminService = Substitute.For<IPointsAdminService>();
            this.pointsController = new PointsController(this.pointsConsumerService, this.pointsAdminService);
        }

        [Test]
        public void GetAllPointsTest()
        {
            this.pointsConsumerService.GetPoints().Returns(this.getMockedPoints());

            IEnumerable<PointDTO> resultPoints = this.pointsController.Get();
            IEnumerable<PointDTO> expectedResult = this.getMockedPoints();

            // assertions
            this.pointsConsumerService.Received().GetPoints();
            CollectionAssert.AreEquivalent(resultPoints, expectedResult);
        }

        [Test]
        public void GetAllPointsEmptyListTest()
        {
            this.pointsConsumerService.GetPoints().Returns(new List<PointDTO>());

            IEnumerable<PointDTO> resultPoints = this.pointsController.Get();
            IList<PointDTO> expectedResult = new List<PointDTO>();

            // assertions
            this.pointsConsumerService.Received().GetPoints();
            CollectionAssert.AreEquivalent(resultPoints, expectedResult);
        }

        [Test]
        public void GetAllPointsNullListTest()
        {
            this.pointsConsumerService.GetPoints().Returns((IEnumerable<PointDTO>) null);

            IEnumerable<PointDTO> resultPoints = this.pointsController.Get();
            IEnumerable<PointDTO> expectedResult = new List<PointDTO>();

            // assertions
            this.pointsConsumerService.Received().GetPoints();
            CollectionAssert.AreEquivalent(resultPoints, expectedResult);
        }


        [Test]
        public void GetPointTest()
        {
            this.pointsConsumerService.GetPoint(Arg.Any<int>()).Returns(this.getMockedPoints().ElementAt(0));

            PointDTO resultPoint = this.pointsController.Get(10);
            PointDTO expectedResult = this.getMockedPoints().ElementAt(0);

            // assertions
            this.pointsConsumerService.Received().GetPoint(10);
            Assert.AreEqual(resultPoint, expectedResult);
        }

        [Test]
        public void GetNonExistingPointTest()
        {
            this.pointsConsumerService.GetPoint(Arg.Any<int>()).Returns((PointDTO)null);

            PointDTO resultPoint = this.pointsController.Get(10);
            PointDTO expectedResult = this.getMockedPoints().ElementAt(0);

            // assertions
            this.pointsConsumerService.Received().GetPoint(10);
            Assert.AreEqual(resultPoint, expectedResult);
        }

        [Test]
        public void PostPointTest()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void PostInvalidPointTest()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void PutPointTest()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void PutInvalidPointTest()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void DeletePointTest()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void DeleteNonExistingPointTest()
        {
            throw new NotImplementedException();
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
