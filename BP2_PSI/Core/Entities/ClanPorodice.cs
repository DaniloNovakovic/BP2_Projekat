using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ClanPorodice
    {
        public IMKU Preminuo { get; set; }

        public Covek Clan { get; set; }

        /// <summary>
        /// Tip veze clana porodice sa preminulim (npr. "Sestra")
        /// </summary>
        public string TipVeze { get; set; }
    }
}