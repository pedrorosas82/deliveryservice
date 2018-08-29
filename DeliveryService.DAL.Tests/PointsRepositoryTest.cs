using DeliveryService.Common.DTOs;
using DeliveryService.Common.Interfaces.DAL;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.DAL.Tests
{
    [TestFixture]
    public class PointsRepositoryTest
    {
        private IPointsRepository pointsRepository;

        [Test]
        public void GetPointTest()
        {

            throw new NotImplementedException();

            PointDTO expectedResult = new PointDTO()
            {
                Id = 1,
                Name = "A"
            };

            PointDTO result = this.pointsRepository.Get(1);

            // assertions
            
        }

        [Test]
        public void GetNonExistentPointTest()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void GetPointsTest()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void GetPointsEmptyListTest()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void SaveExistingPointTest()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void SaveNewPointTest()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void DeleteNonExistentPointTest()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void DeleteExistentPointNotUsedInRoutesTest()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void DeleteExistentPointUsedInRoutesTest()
        {
            throw new NotImplementedException();
        }
    }
}
