using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Manager
    {
        public long WorkerId { get; set; }
        public Worker Worker { get; set; }

        public ICollection<TechnicalStaff> Employees { get; set; } = new HashSet<TechnicalStaff>();
    }
}