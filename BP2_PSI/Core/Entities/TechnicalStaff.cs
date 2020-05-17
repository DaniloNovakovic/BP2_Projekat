using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class TechnicalStaff
    {
        public long WorkerId { get; set; }
        public Worker Worker { get; set; }

        public long? ManagerId { get; set; }
        public Manager Manager { get; set; }
    }
}