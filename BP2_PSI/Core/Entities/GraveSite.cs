namespace Core.Entities
{
    public class GraveSite
    {
        public long DeathRecordId { get; set; }

        /// <summary>
        /// Urn or Coffin
        /// </summary>
        public string Type { get; set; }

        public long LocationId { get; set; }
        public Location Location { get; set; }
        public DeathRecord DeathRecord { get; set; }
    }
}