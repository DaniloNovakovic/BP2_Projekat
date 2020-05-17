using System.Linq;

namespace Persistance
{
    public class ApplicationDbBootstrapper
    {
        private readonly ApplicationDbContext _context;

        public ApplicationDbBootstrapper(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates Database if not exist and seeds data if needed
        /// </summary>
        public void Initialize()
        {
            _context.Database.CreateIfNotExists();
            if (!_context.Managers.Any())
            {
                Seed();
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Seeds the data without calling .SaveChanges
        /// </summary>
        public void Seed()
        {
        }
    }
}