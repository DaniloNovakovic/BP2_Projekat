using System;

namespace Core.Entities
{
    public class DeathRecord
    {
        public long PersonId { get; set; }
        public Person Person { get; set; }
        public DateTime? DeathDate { get; set; }

        public long? GraveSiteId { get; set; }
        public GraveSite GraveSite { get; set; }

        public override string ToString()
        {
            if (Person != null)
            {
                return Person.ToString();
            }
            return $"Id:{PersonId}, Death:{DeathDate}";
        }
    }
}