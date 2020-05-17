using Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Persistance.Configurations
{
    public class CoffinConfiguration : EntityTypeConfiguration<Coffin>
    {
        public CoffinConfiguration()
        {
            HasKey(c => c.GraveSiteId);

            HasRequired(c => c.GraveSite)
                .WithOptional()
                .WillCascadeOnDelete();
        }
    }
}