

using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    /**
 * \brief Клас, що описує дані
 * \author Лілія Оринич
 * \version 1.0
 * \date Листопад 2018
 * \detail Клас містить автовластивості, кожне з яких 
 * зіставляється з окремим стовбцем у базі даних.
 */
    public class Airport
    {
        [Key]
        public int flightID { get; set; }
        public string direction { get; set; }
        public DateTime departureTime { get; set; }
        public DateTime arriveTime { get; set; }
    }
}
