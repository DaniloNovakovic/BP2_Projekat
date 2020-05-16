using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Radnik
    {
        public long CovekId { get; set; }
        public Covek Covek { get; set; }

        public string RadnoVreme { get; set; }

        /// <summary>
        /// Tehnicko Osoblje ili Sef
        /// </summary>
        public string TipRadnika { get; set; }
    }
}