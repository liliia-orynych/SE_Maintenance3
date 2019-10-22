using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Model;

namespace Test
{
    [TestFixture]
    [Category("Model project tests")]
    public class ModelTests
    {
        EFUnitOfWork unitOfWork = new EFUnitOfWork("DbConnection2");

        [TestCase]
        public void GetAllCheck()
        {
            List<Airport> test_list = new List<Airport>();

            Airport air_test1 = new Airport
            {
                direction = "Canada",
                departureTime = new DateTime(2018, 3, 12, 7, 0, 0),
                arriveTime = new DateTime(2008, 3, 12, 10, 0, 0),
            };

            Airport air_test2 = new Airport
            {
                direction = "Italy",
                departureTime = new DateTime(2017, 3, 1, 7, 0, 0),
                arriveTime = new DateTime(2017, 4, 1, 10, 0, 0),
            };

            unitOfWork.Airports.Create(air_test1);
            unitOfWork.Save();
            unitOfWork.Airports.Create(air_test2);
            unitOfWork.Save();

            test_list.Add(air_test1);
            test_list.Add(air_test2);

            List<Airport> result_list = new List<Airport>();
            result_list.AddRange(unitOfWork.Airports.GetAll());
            foreach (Airport air in test_list)
                Assert.IsTrue(result_list.Contains(air));
        }

        [TestCase]
        public void GetCheck()
        {
            Airport air_test1 = new Airport
            {
                direction = "Canada",
                departureTime = new DateTime(2018, 3, 12, 7, 0, 0),
                arriveTime = new DateTime(2008, 3, 12, 10, 0, 0),
            };

            unitOfWork.Airports.Create(air_test1);
            unitOfWork.Save();

            Airport air_test2 = unitOfWork.Airports.Find(air_test1.flightID);
            Airport air_test3 = unitOfWork.Airports.Get(air_test1.flightID);

            Assert.AreEqual(air_test2, air_test3);

            unitOfWork.Airports.Delete(air_test1.flightID);
            unitOfWork.Save();

        }

        [TestCase]
        public void CreateCheck()
        {
            Airport air_test1 = new Airport
            {
                direction = "Canada",
                departureTime = new DateTime(2018, 3, 12, 7, 0, 0),
                arriveTime = new DateTime(2008, 3, 12, 10, 0, 0),
            };

            unitOfWork.Airports.Create(air_test1);
            unitOfWork.Save();

            Airport air_test2 = unitOfWork.Airports.Find(air_test1.flightID);

            Assert.AreEqual(air_test1, air_test2);

            unitOfWork.Airports.Delete(air_test1.flightID);
            unitOfWork.Save();
        }

        [TestCase]
        public void UpdateCheck()
        {
            Airport air_test1 = new Airport
            {
                direction = "Canada",
                departureTime = new DateTime(2018, 3, 12, 7, 0, 0),
                arriveTime = new DateTime(2008, 3, 12, 10, 0, 0),
            };

            unitOfWork.Airports.Create(air_test1);
            unitOfWork.Save();
            Airport air_test2 = unitOfWork.Airports.Find(air_test1.flightID);
            string country = "USA";
            air_test2.direction = country;
            unitOfWork.Airports.Update(air_test2);
            unitOfWork.Save();
            Airport air_test3 = unitOfWork.Airports.Find(air_test2.flightID);

            Assert.AreEqual(air_test3.direction, country);
            unitOfWork.Airports.Delete(air_test1.flightID);
            unitOfWork.Save();
        }
        
        [TestCase]
        public void FindCheck()
        {
            Airport air_test1 = new Airport
            {
                direction = "Canada",
                departureTime = new DateTime(2018, 3, 12, 7, 0, 0),
                arriveTime = new DateTime(2008, 3, 12, 10, 0, 0),
            };

            unitOfWork.Airports.Create(air_test1);
            unitOfWork.Save();

            Airport air_test2 = unitOfWork.Airports.Find(air_test1.flightID);

            Assert.AreEqual(air_test2, air_test1);

            unitOfWork.Airports.Delete(air_test1.flightID);
            unitOfWork.Save();
        }

        [TestCase]
        public void DeleteCheck()
        {
            Airport air_test1 = new Airport
            {
                direction = "Canada",
                departureTime = new DateTime(2018, 3, 12, 7, 0, 0),
                arriveTime = new DateTime(2008, 3, 12, 10, 0, 0),
            };

            unitOfWork.Airports.Create(air_test1);
            unitOfWork.Save();

            Airport air_test2 = unitOfWork.Airports.Find(air_test1.flightID);
            unitOfWork.Airports.Delete(air_test2.flightID);
            unitOfWork.Save();
            Airport air_test3 = unitOfWork.Airports.Get(air_test2.flightID);

            Assert.IsNull(air_test3);

            unitOfWork.Airports.Delete(air_test1.flightID);
            unitOfWork.Save();
        }
    }
}
