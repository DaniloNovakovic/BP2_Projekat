using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class IMKU
    {
        public long CovekId { get; set; }
        public Covek Covek { get; set; }
        public DateTime? DatumSmrti { get; set; }
        public GrobnoMesto GrobnoMesto { get; set; }
    }
}