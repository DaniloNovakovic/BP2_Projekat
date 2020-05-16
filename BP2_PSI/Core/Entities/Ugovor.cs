using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Ugovor
    {
        public ClanPorodice ClanPorodice { get; set; }
        public Sef Sef { get; set; }
        public Kapela Kapela { get; set; }

        public TipSahrane? TipSahrane { get; set; }
        public DateTime? VremeSahrane { get; set; }
    }
}