

using System;
using System.Data.Entity;

namespace Model
{
    /**
 * \brief Клас контексту даних
 * \author Лілія Оринич
 * \version 1.0
 * \date Листопад 2018
 * \detail Клас для взаємодії з базою даних
 */
    public class AirportContext : DbContext
    {
        public AirportContext(string connectionString)
            : base("DbConnection1") { }

        public DbSet<Airport> Airports { get; set; }
        Type providerService = typeof(System.Data.Entity.SqlServer.SqlProviderServices);
    }
}
