using Core.Entities;
using Persistance.Configurations;
using System.Data.Entity;

namespace Persistance
{
    public class ApplicationDbContext : DbContext
    {
        #region ctors

        /// <summary>
        /// Constructs a new context instance using conventions to create the name of the database
        /// to which a connection will be made. The by-convention name is the full name (namespace +
        /// class name) of the derived context class. See the class remarks for how this is used to
        /// create a connection.
        /// </summary>
        public ApplicationDbContext() : base("DefaultConnection")
        {
        }

        /// <summary>
        /// Constructs a new context instance using the given string as the name or connection
        /// string for the database to which a connection will be made. See the class remarks for
        /// how this is used to create a connection.
        /// </summary>
        /// <param name="nameOrConnectionString">Either the database name or a connection string.</param>
        public ApplicationDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        #endregion ctors

        public DbSet<DeathRecord> DeathRecords { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<TechnicalStaff> TechnicalStaff { get; set; }
        public DbSet<GraveSite> GraveSites { get; set; }
        public DbSet<Chapel> Chapels { get; set; }
        public DbSet<Contract> Contracts { get; set; }

        public DbSet<Coffin> Coffins { get; set; }
        public DbSet<Urn> Urns { get; set; }

        public DbSet<FamilyMember> FamilyMembers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(typeof(DeathRecordConfiguration).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}