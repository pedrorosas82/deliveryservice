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
            this.pointsConsumerService.GetPoints();

            // assertions
            this.pointsRepository.Received().GetPoints();
        }

        [Test]
        public void GetPointTest()
        {
            this.pointsConsumerService.GetPoint(5);

            // assertions
            this.pointsRepository.Received().GetPoint(5);
        }

        [Test]
        public void GetPointTestZeroIdTest()
        {
            this.validateExceptionThrownOnGetPointWithBadArgument(0);
        }

        [Test]
        public void GetPointTestNegativeIdTest()
        {
            this.validateExceptionThrownOnGetPointWithBadArgument(-1);
        }



        private void validateExceptionThrownOnGetPointWithBadArgument(int pointId)
        {
            var thrownException = Assert.Throws<ArgumentException>(() => this.pointsConsumerService.GetPoint(pointId));

            // assertions
            this.pointsRepository.DidNotReceive().GetPoint(Arg.Any<int>());
            Assert.That(thrownException.Message, Is.EqualTo("Point Id must be an integer greater than 0."));
        }
    }
}
