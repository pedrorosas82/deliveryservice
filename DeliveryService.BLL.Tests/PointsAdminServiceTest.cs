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
    public class PointsAdminServiceTest
    {
        private IPointsAdminService pointsAdminService;
        private IPointsRepository pointsRepository;

        [SetUp]
        public void SetupBeforeEachTest()
        {
            this.pointsRepository = Substitute.For<IPointsRepository>();
            this.pointsAdminService = new PointsAdminService(this.pointsRepository);
        }

        [Test]
        public void CreatePointTest()
        {
            PointDTO point = new PointDTO()
            {
                Name = "Test"
            };

            this.pointsAdminService.CreatePoint(point);

            // assertions
            this.pointsRepository.Received().Save(point);
        }

        [Test]
        public void CreatePointWithIdTest()
        {
            PointDTO point = new PointDTO()
            {
                Id = 5,
                Name = "Test"
            };

            var thrownException = Assert.Throws<ArgumentException>(() => this.pointsAdminService.CreatePoint(point));

            // assertions
            this.pointsRepository.DidNotReceive().Save(Arg.Any<PointDTO>());
            Assert.That(thrownException.Message, Is.EqualTo("Point Id is assigned automatically. Cannot create point with the Id assigned."));
        }

        [Test]
        public void UpdatePointTest()
        {
            PointDTO point = new PointDTO()
            {
                Id = 5,
                Name = "Test"
            };

            this.pointsAdminService.UpdatePoint(point);

            // assertions
            this.pointsRepository.Received().Save(point);
        }

        [Test]
        public void UpdatePointZeroIdTest()
        {
            PointDTO point = new PointDTO()
            {
                Id = 0,
                Name = "Test"
            };

            this.validateExceptionThrownOnUpdateWithBadArgument(point);
        }

        [Test]
        public void UpdatePointNegativeIdTest()
        {
            PointDTO point = new PointDTO()
            {
                Id = -1,
                Name = "Test"
            };

            this.validateExceptionThrownOnUpdateWithBadArgument(point);
        }

        [Test]
        public void DeletePointTest()
        {
            this.pointsAdminService.DeletePoint(5);

            // assertions
            this.pointsRepository.Received().Delete(5);
        }

        [Test]
        public void DeletePointZeroIdArgumentTest()
        {
            this.validateExceptionThrownOnDeleteWithBadArgument(0);
        }

        [Test]
        public void DeletePointNegativeIdArgumentTest()
        {
            this.validateExceptionThrownOnDeleteWithBadArgument(-1);
        }


        private void validateExceptionThrownOnDeleteWithBadArgument(int pointId)
        {
            var thrownException = Assert.Throws<ArgumentException>(() => this.pointsAdminService.DeletePoint(pointId));

            // assertions
            this.pointsRepository.DidNotReceive().Delete(Arg.Any<int>());
            Assert.That(thrownException.Message, Is.EqualTo("Point Id must be an integer greater than 0."));
        }

        private void validateExceptionThrownOnUpdateWithBadArgument(PointDTO point)
        {
            var thrownException = Assert.Throws<ArgumentException>(() => this.pointsAdminService.UpdatePoint(point));

            // assertions
            this.pointsRepository.DidNotReceive().Save(Arg.Any<PointDTO>());
            Assert.That(thrownException.Message, Is.EqualTo("Point Id must be an integer greater than 0."));
        }
    }
}
