using Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Persistance.Configurations
{
    public class ManagerConfiguration : EntityTypeConfiguration<Manager>
    {
        public ManagerConfiguration()
        {
            HasKey(m => m.WorkerId);

            HasRequired(w => w.Worker)
                .WithOptional()
                .WillCascadeOnDelete();

            HasMany(w => w.Employees)
                .WithOptional(ts => ts.Manager)
                .HasForeignKey(ts => ts.ManagerId);
        }
    }
}