using Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Persistance.Configurations
{
    public class FamilyMemberConfiguration : EntityTypeConfiguration<FamilyMember>
    {
        public FamilyMemberConfiguration()
        {
            HasRequired(fm => fm.Member)
                .WithMany()
                .HasForeignKey(fm => fm.MemberId)
                .WillCascadeOnDelete(false);

            HasRequired(fm => fm.RelatedTo)
                .WithMany()
                .HasForeignKey(fm => fm.RelatedToId)
                .WillCascadeOnDelete(false);
        }
    }
}