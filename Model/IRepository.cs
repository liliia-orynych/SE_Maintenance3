

using System;
using System.Collections.Generic;

namespace Model
{
    /**
 * \brief Інтерфейс репозиторія
 * \author Лілія Оринич
 * \version 1.0
 * \date Листопад 2018
 * \detail Інтерфейс дає можливість абстрагуватися від 
 * конкретних підлючень до джерела даних.
 */
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        T Find(int num);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
