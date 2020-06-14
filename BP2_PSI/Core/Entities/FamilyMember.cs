namespace Core.Entities
{
    public class FamilyMember
    {
        public long Id { get; set; }

        public long? RelatedToId { get; set; }
        public DeathRecord RelatedTo { get; set; }

        public long? MemberId { get; set; }
        public Person Member { get; set; }

        /// <summary>
        /// Relation type of family member with the deceased one (ex. "sister")
        /// </summary>
        public string RelationType { get; set; }

        public override string ToString()
        {
            return $"L:{MemberId}, D:{RelatedToId}, T:{RelationType}";
        }
    }
}