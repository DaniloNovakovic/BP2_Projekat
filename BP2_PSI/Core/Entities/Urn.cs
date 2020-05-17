using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Urn
    {
        public long GraveSiteId { get; set; }
        public GraveSite GraveSite { get; set; }

        public double Capacity { get; set; }
    }
}