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

        }

        [Test]
        public void GetPointsTest()
        {

        }

        [Test]
        public void GetPointsEmptyListTest()
        {

        }

        [Test]
        public void SaveExistingPointTest()
        {

        }

        [Test]
        public void SaveNewPointTest()
        {

        }

        [Test]
        public void DeleteNonExistentPointTest()
        {

        }

        [Test]
        public void DeleteExistentPointNotUsedInRoutesTest()
        {

        }

        [Test]
        public void DeleteExistentPointUsedInRoutesTest()
        {

        }
    }
}
