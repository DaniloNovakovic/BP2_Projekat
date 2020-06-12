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

        public void Initialize()
        {
            using (var context = new ApplicationDbContext(_nameOrConnectionString))
            {
                var bootstrapper = new ApplicationDbBootstrapper(context);
                bootstrapper.Initialize();
            }
        }

        public IUnitOfWork CreateNew()
        {
            var context = new ApplicationDbContext(_nameOrConnectionString);
            return new UnitOfWork(context);
        }
    }
}