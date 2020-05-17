namespace Core.Entities
{
    public class FamilyMember
    {
        public long Id { get; set; }
        public DeathRecord RelatedTo { get; set; }

        public Person Member { get; set; }

        /// <summary>
        /// Relation type of family member with the deceased one (ex. "sister")
        /// </summary>
        public string RelationType { get; set; }
    }
}