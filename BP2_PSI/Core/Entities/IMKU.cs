﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class IMKU
    {
        public long PersonId { get; set; }
        public Person Person { get; set; }
        public DateTime? DeathDate { get; set; }
        public GraveSite GraveSite { get; set; }
    }
}