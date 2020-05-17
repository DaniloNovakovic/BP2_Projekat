using Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Persistance.Configurations
{
    public class UrnConfiguration : EntityTypeConfiguration<Urn>
    {
        public UrnConfiguration()
        {
            HasKey(u => u.GraveSiteId);

            HasRequired(u => u.GraveSite)
                .WithOptional()
                .WillCascadeOnDelete();
        }
    }
}