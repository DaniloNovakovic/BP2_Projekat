namespace Core.Entities
{
    public class Worker
    {
        public long PersonId { get; set; }
        public Person Person { get; set; }

        public string WorkTime { get; set; }

        /// <summary>
        /// Tehnicko Osoblje ili Sef
        /// </summary>
        public string Role { get; set; }

        public override string ToString()
        {
            if (Person != null)
            {
                return Person.ToString() + $", {Role}";
            }
            return $"Id:{PersonId}, {Role}, {WorkTime}";
        }
    }
}