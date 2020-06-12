using Core.Entities;
using System.Collections.Generic;

namespace Core.Interfaces.Repositories
{
    public interface IDeathRecordRepository : IRepository<DeathRecord>
    {
        IEnumerable<DeathRecord> GetAllUnburied();
    }
}