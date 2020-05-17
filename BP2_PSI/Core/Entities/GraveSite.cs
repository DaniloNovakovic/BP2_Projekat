using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class GraveSite
    {
        public long Id { get; set; }

        /// <summary>
        /// Urn or Coffin
        /// </summary>
        public string Type { get; set; }

        public Location Location { get; set; }
        public DeathRecord DeathRecord { get; set; }
    }
}