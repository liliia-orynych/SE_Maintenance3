

using System;

namespace Model
{
    /**
 * \brief Інтерфейс паттерну UnitOfWork
 * \author Лілія Оринич
 * \version 1.0
 * \date Листопад 2018
 */
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Airport> Airports { get; }
        void Save();
    }
}
