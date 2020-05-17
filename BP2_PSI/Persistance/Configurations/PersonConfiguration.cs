using Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Persistance.Configurations
{
    public class PersonConfiguration : EntityTypeConfiguration<Person>
    {
        public PersonConfiguration()
        {
        }
    }
}