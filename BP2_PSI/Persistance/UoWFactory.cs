using Core.Interfaces;

namespace Persistance
{
    public class UoWFactory : IUoWFactory
    {
        private readonly string _nameOrConnectionString;

        public UoWFactory(string nameOrConnectionString = "DefaultConnection")
        {
            _nameOrConnectionString = nameOrConnectionString;
        }

        public IUnitOfWork CreateNew(bool seedDatabase = false)
        {
            var context = new ApplicationDbContext(_nameOrConnectionString);

            if (seedDatabase)
            {
                var bootstrapper = new ApplicationDbBootstrapper(context);
                bootstrapper.Initialize();
            }

            return new UnitOfWork(context);
        }
    }
}