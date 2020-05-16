using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Kovceg
    {
        public long GrobnoMestoId { get; set; }
        public GrobnoMesto GrobnoMesto { get; set; }

        public string Marka { get; set; }
    }
}