using Core.Entities;
using Core.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Persistance.Repositories
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        private readonly ApplicationDbContext _db;

        public PersonRepository(ApplicationDbContext context) : base(context)
        {
            _db = context;
        }

        public IEnumerable<Person> GetAlivePersons()
        {
            var deathRecordIds = _db.DeathRecords.Select(dr => dr.PersonId).ToList();
            var persons = _db.Persons.ToList();
            return persons.Where(p => !deathRecordIds.Any(id => id == p.Id)).ToList();
        }
    }
}