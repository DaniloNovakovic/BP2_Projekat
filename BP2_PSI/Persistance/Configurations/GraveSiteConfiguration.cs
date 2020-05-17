using Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Persistance.Configurations
{
    public class GraveSiteConfiguration : EntityTypeConfiguration<GraveSite>
    {
        public GraveSiteConfiguration()
        {
            HasRequired(gs => gs.DeathRecord)
                .WithOptional(dr => dr.GraveSite)
                .WillCascadeOnDelete();

            HasRequired(gs => gs.Location)
                .WithOptional()
                .WillCascadeOnDelete();

            Property(gs => gs.Type).IsRequired();
        }
    }
}