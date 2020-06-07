using Core.Entities;
using System.Collections.Generic;

namespace Core.Interfaces.Repositories
{
    public interface IPersonRepository : IRepository<Person>
    {
        IEnumerable<Person> GetAlivePersons();
    }
}