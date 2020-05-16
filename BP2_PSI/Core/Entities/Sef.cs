using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Sef
    {
        public long RadnikId { get; set; }
        public Radnik Radnik { get; set; }

        public ICollection<TehnickoOsoblje> Zaposleni { get; set; } = new HashSet<TehnickoOsoblje>();
    }
}