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
            var thrownException = Assert.Throws<HttpResponseException>(() => this.pointsController.Get(10));

            // assertions
            this.pointsConsumerService.Received().GetPoint(Arg.Any<int>());
            Assert.AreEqual(thrownException.Response.StatusCode, HttpStatusCode.BadRequest);
        }

        [Test]
        public void PostPointTest()
        {
            PointDTO mockedPoint = this.getMockedPoints().ElementAt(0);
            this.pointsAdminService.CreatePoint(Arg.Any<PointDTO>()).Returns(mockedPoint);

            PointDTO point = new PointDTO()
            {
                Name = "A"
            };

            IHttpActionResult actionResult = this.pointsController.Post(point);
            PointDTO expectedResult = mockedPoint;

            // assertions
            this.pointsAdminService.Received().CreatePoint(point);
            OkNegotiatedContentResult<PointDTO> contentResult = actionResult as OkNegotiatedContentResult<PointDTO>;

            Assert.IsNotNull(contentResult);
            Assert.AreEqual(expectedResult, contentResult.Content);
        }

        [Test]
        public void PostInvalidPointTest()
        {
            PointDTO mockedPoint = this.getMockedPoints().ElementAt(0);
            this.pointsAdminService.UpdatePoint(Arg.Any<PointDTO>()).Returns(mockedPoint);

            PointDTO point = new PointDTO()
            {
                Id = 1,
                Name = "A"
            };

            IHttpActionResult actionResult = this.pointsController.Post(point);

            // assertions
            this.pointsAdminService.DidNotReceive().CreatePoint(point);
            BadRequestErrorMessageResult badRequestMessage = actionResult as BadRequestErrorMessageResult;

            Assert.IsNotNull(badRequestMessage);
            Assert.AreEqual(badRequestMessage.Message, "Point Id cannot be defined for new entity.");
        }

        [Test]
        public void PutPointTest()
        {
            PointDTO mockedPoint = this.getMockedPoints().ElementAt(0);
            this.pointsAdminService.UpdatePoint(Arg.Any<PointDTO>()).Returns(mockedPoint);

            PointDTO point = new PointDTO()
            {
                Id = 1,
                Name = "A"
            };

            IHttpActionResult actionResult = this.pointsController.Put(point);
            PointDTO expectedResult = mockedPoint;

            // assertions
            this.pointsAdminService.Received().UpdatePoint(point);
            OkNegotiatedContentResult<PointDTO> contentResult = actionResult as OkNegotiatedContentResult<PointDTO>;

            Assert.IsNotNull(contentResult);
            Assert.AreEqual(expectedResult, contentResult.Content);
        }

        [Test]
        public void PutInvalidPointTest()
        {
            PointDTO mockedPoint = this.getMockedPoints().ElementAt(0);
            this.pointsAdminService.UpdatePoint(Arg.Any<PointDTO>()).Returns(mockedPoint);

            PointDTO point = new PointDTO()
            {
                Name = "A"
            };

            IHttpActionResult actionResult = this.pointsController.Put(point);

            // assertions
            this.pointsAdminService.DidNotReceive().UpdatePoint(point);
            BadRequestErrorMessageResult badRequestMessage = actionResult as BadRequestErrorMessageResult;

            Assert.IsNotNull(badRequestMessage);
            Assert.AreEqual(badRequestMessage.Message, "Point Id must be greater than 0.");
        }

        [Test]
        public void DeletePointTest()
        {
            this.pointsAdminService.When(x => x.DeletePoint(Arg.Any<int>()))
                                   .Do(x => { return; });

            this.pointsController.Delete(10);

            // assertions
            this.pointsAdminService.Received().DeletePoint(10);
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
