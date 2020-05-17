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
        public ApplicationDbContext() : base()
        {
        }

        /// <summary>
        /// Constructs a new context instance using conventions to create the name of the database
        /// to which a connection will be made, and initializes it from the given model. The
        /// by-convention name is the full name (namespace + class name) of the derived context
        /// class. See the class remarks for how this is used to create a connection.
        /// </summary>
        /// <param name="model">The model that will back this context.</param>
        public ApplicationDbContext(System.Data.Entity.Infrastructure.DbCompiledModel model) : base(model)
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

        /// <summary>
        /// Constructs a new context instance using the given string as the name or connection
        /// string for the database to which a connection will be made, and initializes it from the
        /// given model. See the class remarks for how this is used to create a connection.
        /// </summary>
        /// <param name="nameOrConnectionString">Either the database name or a connection string.</param>
        /// <param name="model">The model that will back this context.</param>
        public ApplicationDbContext(string nameOrConnectionString, System.Data.Entity.Infrastructure.DbCompiledModel model) : base(nameOrConnectionString, model)
        {
        }

        /// <summary>
        /// Constructs a new context instance using the existing connection to connect to a
        /// database. The connection will not be disposed when the context is disposed if <paramref
        /// name="contextOwnsConnection"/> is <c>false</c>.
        /// </summary>
        /// <param name="existingConnection">An existing connection to use for the new context.</param>
        /// <param name="contextOwnsConnection">
        /// If set to <c>true</c> the connection is disposed when the context is disposed, otherwise
        /// the caller must dispose the connection.
        /// </param>
        public ApplicationDbContext(System.Data.Common.DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection)
        {
        }

        /// <summary>
        /// Constructs a new context instance using the existing connection to connect to a
        /// database, and initializes it from the given model. The connection will not be disposed
        /// when the context is disposed if <paramref name="contextOwnsConnection"/> is <c>false</c>.
        /// </summary>
        /// <param name="existingConnection">An existing connection to use for the new context.</param>
        /// <param name="model">The model that will back this context.</param>
        /// <param name="contextOwnsConnection">
        /// If set to <c>true</c> the connection is disposed when the context is disposed, otherwise
        /// the caller must dispose the connection.
        /// </param>
        public ApplicationDbContext(System.Data.Common.DbConnection existingConnection, System.Data.Entity.Infrastructure.DbCompiledModel model, bool contextOwnsConnection) : base(existingConnection, model, contextOwnsConnection)
        {
        }

        /// <summary>
        /// Constructs a new context instance around an existing ObjectContext.
        /// </summary>
        /// <param name="objectContext">An existing ObjectContext to wrap with the new context.</param>
        /// <param name="dbContextOwnsObjectContext">
        /// If set to <c>true</c> the ObjectContext is disposed when the DbContext is disposed,
        /// otherwise the caller must dispose the connection.
        /// </param>
        public ApplicationDbContext(System.Data.Entity.Core.Objects.ObjectContext objectContext, bool dbContextOwnsObjectContext) : base(objectContext, dbContextOwnsObjectContext)
        {
        }

        #endregion ctors

        public DbSet<FamilyMember> FamilyMembers { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<GraveSite> GraveSites { get; set; }
        public DbSet<DeathRecord> DeathRecords { get; set; }
        public DbSet<Chapel> Chapels { get; set; }
        public DbSet<Coffin> Coffins { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<TechnicalStaff> TechnicalStaff { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Urn> Urns { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(typeof(DeathRecordConfiguration).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}