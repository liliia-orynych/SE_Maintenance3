
using System;
using System.Collections.Generic;
using Model;

namespace ControlLib
{
/**
 * \brief Клас, що описує бізнес-логіку
 * \author Лілія Оринич
 * \version 1.0
 * \date Листопад 2018
 * \detail Клас містить методи для додавання, видалення, пошуку
 * та сортування даних.
 */
    public class Manipulation
    {
        IUnitOfWork db { get; set; }

        /// Створює прив'язку даних
        public Manipulation()
        {
            db = new EFUnitOfWork(" ");
        }

        public Manipulation(string connectionString)
        {
            db = new EFUnitOfWork(connectionString);
        }

        /// Знаходить рейс за індексом
        public Airport FindFlight(int n)
        {
            return db.Airports.Find(n);
        }

        /// Виводить список всіх рейсів
        public List<Airport> GetList()
        {
            List<Airport> result = new List<Airport>();
            foreach (Airport r in db.Airports.GetAll())
                result.Add(r);
            return result;
        }

        /// Додає рейс
        public void FlightAddition(AirportModel air)
        {
            db.Airports.Create(new Airport
            {
                direction = air.direction,
                departureTime = air.departureTime,
                arriveTime = air.arriveTime             
            });
            db.Save();
        }

        /// Видаляє рейс за індексом
        public void FlightDeleting(int index)
        {
            db.Airports.Delete(index);
            db.Save();
        }

        /**
         * Відсортовує рейси за часом
         * \return Список рейсів, відсортований
         * по зростанню за часом відправлення 
         */
        public List<Airport> sortByTime()
        {
            List<Airport> airports = new List<Airport>();
            foreach (Airport r in db.Airports.GetAll())
                airports.Add(r);

            int size = airports.Count;
            for (int i = 0; i < size; i++)
            {
                Airport current = airports[i];
                int j = i;
                while (j > 0 && current.departureTime.TimeOfDay < airports[j-1].departureTime.TimeOfDay)
                {
                    airports[j] = airports[j - 1];
                    j--;
                }
                airports[j] = current;
            }

            return airports;
        }

        /**
         * Знаходить рейси за напрямом
         * \param [in] dir Напрям польоту
         * \return Список рейсів, що відлітають до заданого напрямку
         */
        public List<Airport> selectByDirection(string dir)
        {
            List<Airport> airports = new List<Airport>();
            foreach (Airport r in db.Airports.GetAll())
                airports.Add(r);

            List<Airport> flightByDirection = new List<Airport>();

            foreach (Airport airport in airports)
            {
                if (airport != null && airport.direction == dir)
                {
                    flightByDirection.Add(airport);
                }
            }

            return flightByDirection;
        }

        /**
         * Знаходить рейси за часом відправлення
         * \return Список рейсів, що відлітають між 00:00 та 06:00
         */
        public List<Airport> selectByTime()
        {
            TimeSpan dt1 = new TimeSpan(0, 0, 0);
            TimeSpan dt2 = new TimeSpan(7, 0, 0);
            List<Airport> airports = new List<Airport>();
            foreach (Airport r in db.Airports.GetAll())
                airports.Add(r);

            List<Airport> flightByTime = new List<Airport>();

            foreach (Airport airport in airports)
            {
                if (airport != null && airport.departureTime.TimeOfDay > dt1 && airport.departureTime.TimeOfDay < dt2)
                {
                    flightByTime.Add(airport);
                }
            }

            return flightByTime;
        }
        
         /**
          * Знаходить рейси, що відлітають у певному напрямі
          * \return Список рейсів, що відлітають до Турції
          */
        public List<Airport> selectByCountry()
        {
            List<Airport> airports = new List<Airport>();
            foreach (Airport r in db.Airports.GetAll())
                airports.Add(r);

            List<Airport> flightByCountry = new List<Airport>();

            foreach (Airport airport in airports)
            {
                if (airport != null && airport.direction == "Turkey")
                {
                    flightByCountry.Add(airport);
                }
            }
            return flightByCountry;
        }
    }
}
