using Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Persistance.Configurations
{
    public class FamilyMemberConfiguration : EntityTypeConfiguration<FamilyMember>
    {
        public FamilyMemberConfiguration()
        {
            HasKey(fm => new { fm.MemberId, fm.RelatedToId });

            HasRequired(fm => fm.Member)
                .WithMany()
                .HasForeignKey(fm => fm.MemberId);

            HasRequired(fm => fm.RelatedTo)
                .WithMany()
                .HasForeignKey(fm => fm.RelatedToId);
        }
    }
}