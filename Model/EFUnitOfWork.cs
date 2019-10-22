

using System;

namespace Model
{
    /**
 * \brief Клас, що реалізує інтерфейс
 * \author Лілія Оринич
 * \version 1.0
 * \date Листопад 2018
 * \detail Клас реалізує інтерфейс IUnitOfWork, дозволяє спростити роботу з репозиторієм.
 */
    public class EFUnitOfWork : IUnitOfWork
    {
        private AirportContext db;
        private AirportRepository AirportRepository;

        public EFUnitOfWork(string connectionString)
        {
            db = new AirportContext(connectionString);
        }
        public IRepository<Airport> Airports
        {
            get
            {
                if (AirportRepository == null)
                    AirportRepository = new AirportRepository(db);
                return AirportRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
