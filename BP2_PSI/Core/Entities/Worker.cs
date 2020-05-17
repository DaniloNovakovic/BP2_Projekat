using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}