using Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Persistance.Configurations
{
    public class ContractConfiguration : EntityTypeConfiguration<Contract>
    {
        public ContractConfiguration()
        {
            HasRequired(c => c.FamilyMember)
                .WithMany()
                .HasForeignKey(c => c.FamilyMemberId)
                .WillCascadeOnDelete();

            HasRequired(c => c.Manager)
                .WithMany()
                .HasForeignKey(c => c.ManagerId)
                .WillCascadeOnDelete();

            HasRequired(c => c.Chapel)
                .WithMany()
                .HasForeignKey(c => c.ChapelId)
                .WillCascadeOnDelete();
        }
    }
}