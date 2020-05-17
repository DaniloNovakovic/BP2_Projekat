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
                .WillCascadeOnDelete();

            HasRequired(c => c.Manager)
                .WithMany()
                .WillCascadeOnDelete();

            HasRequired(c => c.Chapel)
                .WithMany()
                .WillCascadeOnDelete();
        }
    }
}