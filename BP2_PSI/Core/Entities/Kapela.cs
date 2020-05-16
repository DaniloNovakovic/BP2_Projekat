using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Kapela
    {
        public long Id { get; set; }
        public string Naziv { get; set; }
        public Lokacija Lokacija { get; set; }
    }
}