using System.Collections.Generic;

namespace Core.Entities
{
    public class Manager
    {
        public long WorkerId { get; set; }
        public Worker Worker { get; set; }

        public ICollection<TechnicalStaff> Employees { get; set; } = new HashSet<TechnicalStaff>();

        public override string ToString()
        {
            if (Worker != null)
            {
                return Worker.ToString();
            }
            return $"Id:{WorkerId}";
        }
    }
}