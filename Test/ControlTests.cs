using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ControlLib;
using Model;

namespace Test
{
    [TestFixture]
    [Category("Control project tests")]
    public class ControlTests
    {
        Manipulation m = new Manipulation("DbConnection1");

        [TestCase, MaxTime(5)]
        public void GetListCheck()
        {
            List<AirportModel> test_list = new List<AirportModel>();

            AirportModel air_test1 = new AirportModel
            {
                direction = "Canada",
                departureTime = new DateTime(2018, 3, 12, 7, 0, 0),
                arriveTime = new DateTime(2008, 3, 12, 10, 0, 0),
            };

            AirportModel air_test2 = new AirportModel
            {
                direction = "Italy",
                departureTime = new DateTime(2017, 3, 1, 7, 0, 0),
                arriveTime = new DateTime(2017, 4, 1, 10, 0, 0),
            };

            test_list.Add(air_test1);
            test_list.Add(air_test2);

            CollectionAssert.IsNotSubsetOf(test_list, m.GetList());
        }

        [TestCase ("Japan")]
        public void FlightAdditionCheck(string dir)
        {
            AirportModel airportModel = new AirportModel
            {
                direction = dir,
                departureTime = new DateTime(2008, 3, 1, 7, 0, 0),
                arriveTime = new DateTime(2008, 3, 1, 10, 0, 0),
            };

            m.FlightAddition(airportModel);
            CollectionAssert.IsNotEmpty(m.selectByDirection(airportModel.direction));
        }

        [TestCase]
        [Ignore ("This test is ignored")]
        public void sortByTimeCheck()
        {
            List<Airport> test_list = new List<Airport>();
            List<Airport> result_list = new List<Airport>();
            test_list = m.sortByTime();
            result_list = m.GetList().OrderBy(x => x.departureTime).ToList();
            CollectionAssert.AreEqual(test_list, result_list);  
        }

        [TestCase]
        public void selectByDirectionCheck()
        {
            AirportModel airportModel = new AirportModel
            {
                direction = "Japan",
                departureTime = new DateTime(2008, 3, 1, 7, 0, 0),
                arriveTime = new DateTime(2008, 3, 1, 10, 0, 0),
            };

            m.FlightAddition(airportModel);

            foreach (Airport air in m.selectByDirection("Japan"))
                Assert.AreEqual(airportModel.direction, air.direction);
        }

        [TestCase]
        public void selectByTimeCheck()
        {
            AirportModel airportModel = new AirportModel
            {
                direction = "Japan",
                departureTime = new DateTime(2008, 3, 1, 4, 0, 0),
                arriveTime = new DateTime(2008, 3, 1, 5, 0, 0),
            };

            m.FlightAddition(airportModel);
            foreach (Airport air in m.selectByTime())
                Assert.Contains(air, m.selectByTime());
        }

        [TestCase]
        public void selectByCountryCheck()
        {
            foreach (Airport air in m.selectByCountry())
                Assert.AreEqual("Turkey", air.direction);
        }

        [TestCase]
        [Ignore("This test is ignored")]
        public void FindFlightCheck()
        {
            AirportModel air_test1 = new AirportModel
            {
                direction = "Japan",
                departureTime = new DateTime(2008, 3, 1, 7, 0, 0),
                arriveTime = new DateTime(2008, 3, 1, 10, 0, 0),
            };

            m.FlightAddition(air_test1);
            foreach (Airport air in m.GetList())
                Assert.Contains(air, m.GetList());

        }
    }
}
