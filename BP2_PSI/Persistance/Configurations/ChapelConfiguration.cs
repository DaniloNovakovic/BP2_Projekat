using Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Persistance.Configurations
{
    public class ChapelConfiguration : EntityTypeConfiguration<Chapel>
    {
        public ChapelConfiguration()
        {
            HasRequired(c => c.Location)
                .WithOptional()
                .WillCascadeOnDelete();
        }
    }
}