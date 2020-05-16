using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Lokacija
    {
        public long Id { get; set; }

        /// <summary>
        /// Latitude
        /// </summary>
        public double GeografskaSirina { get; set; }

        /// <summary>
        /// Longitude
        /// </summary>
        public double GeografskaDuzina { get; set; }
    }
}