using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Location
    {
        public long Id { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}