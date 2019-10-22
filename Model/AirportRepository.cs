using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
/**
 * \brief Клас, що реалізує інтерфейс 
 * \author Лілія Оринич
 * \version 1.0
 * \date Листопад 2018
 * \detail Клас реалізує інтерфейс IRepository, є проміжним між
 * між класами, що взаємодіють з даними, та рештою програми. 
 */
    public class AirportRepository : IRepository<Airport>
    {
        private AirportContext db;

        public AirportRepository (AirportContext context)
        {
            this.db = context;
        }

        /// Отримує всі об'єкти
        public IEnumerable<Airport> GetAll()
        {
            return db.Airports;
        }

        /// Отримує об'єкт за індексом
        public Airport Get(int id)
        {
            return db.Airports.Find(id);
        }

        /// Створює новий об'єкт
        public void Create(Airport air)
        {
            db.Airports.Add(air);
        }

        /// Оновлює об'єкт
        public void Update(Airport air)
        {
            db.Entry(air).State = EntityState.Modified;
        }

        /// Отримує об'єкт за ключем
        public Airport Find(int num)
        {
            return db.Airports.First(r => r.flightID == num);
        }

        /// Видаляє об'єкт за ключем
        public void Delete(int flightID)
        {
            Airport air = db.Airports.Find(flightID);
            if (air != null)
                db.Airports.Remove(air);
        }
    }

}
