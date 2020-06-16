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

        public IEnumerable<Person> GetUnemployeedPersons()
        {
            var workerIds = _db.Workers.Select(w => w.PersonId).ToList();
            var persons = _db.Persons.ToList();
            return persons.Where(p => !workerIds.Any(id => id == p.Id)).ToList();
        }

        public override void Remove(Person entity)
        {
            var members = _db.FamilyMembers.Where(fm => fm.MemberId == entity.Id);
            _db.FamilyMembers.RemoveRange(members);
            _db.SaveChanges();

            _db.Persons.Remove(entity);
        }
    }
}