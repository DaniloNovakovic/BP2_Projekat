using Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Persistance.Configurations
{
    public class WorkerConfiguration : EntityTypeConfiguration<Worker>
    {
        public WorkerConfiguration()
        {
            HasKey(w => w.PersonId);

            Property(w => w.Role).IsRequired();

            HasRequired(w => w.Person)
                .WithOptional()
                .WillCascadeOnDelete();
        }
    }
}