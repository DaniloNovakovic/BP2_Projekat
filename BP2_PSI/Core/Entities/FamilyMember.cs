using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class FamilyMember
    {
        public IMKU RelatedTo { get; set; }

        public Person Member { get; set; }

        /// <summary>
        /// Relation type of family member with the deceased one (ex. "sister")
        /// </summary>
        public string RelationType { get; set; }
    }
}