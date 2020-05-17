using Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Persistance.Configurations
{
    public class TechnicalStaffConfiguration : EntityTypeConfiguration<TechnicalStaff>
    {
        public TechnicalStaffConfiguration()
        {
            HasKey(m => m.WorkerId);

            HasRequired(w => w.Worker)
                .WithOptional()
                .WillCascadeOnDelete();
        }
    }
}