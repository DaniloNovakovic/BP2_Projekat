using System;

namespace Core.Entities
{
    public class DeathRecord
    {
        public long PersonId { get; set; }
        public Person Person { get; set; }
        public DateTime? DeathDate { get; set; }

        public long GraveSiteId { get; set; }
        public GraveSite GraveSite { get; set; }
    }
}