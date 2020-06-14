using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Chapel
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public long LocationId { get; set; }
        public Location Location { get; set; }

        public override string ToString()
        {
            return $"{Id}-{Name}";
        }
    }
}