using Core.Interfaces;

namespace Persistance
{
    public class UoWFactory
    {
        private readonly string _nameOrConnectionString;

        public UoWFactory(string nameOrConnectionString = "DefaultConnection")
        {
            _nameOrConnectionString = nameOrConnectionString;
        }

        public IUnitOfWork CreateNew()
        {
            var context = new ApplicationDbContext(_nameOrConnectionString);

            var bootstrapper = new ApplicationDbBootstrapper(context);
            bootstrapper.Initialize();

            return new UnitOfWork(context);
        }
    }
}