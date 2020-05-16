using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class GrobnoMesto
    {
        public long Id { get; set; }

        /// <summary>
        /// Urna ili Kovceg
        /// </summary>
        public string Tip { get; set; }

        public Lokacija Lokacija { get; set; }
        public IMKU IMKU { get; set; }
    }
}