using Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Persistance.Repositories
{
    public class DeathRecordRepository : Repository<DeathRecord>
    {
        private readonly ApplicationDbContext _db;

        public DeathRecordRepository(ApplicationDbContext context) : base(context)
        {
            _db = context;
        }

        public override DeathRecord Get(params object[] keyValues)
        {
            long key = (long)keyValues[0];
            return _db.DeathRecords.Include(r => r.Person).SingleOrDefault(r => r.PersonId == key);
        }

        public override DeathRecord Get(Expression<Func<DeathRecord, bool>> filter)
        {
            return _db.DeathRecords.Include(r => r.Person).FirstOrDefault(filter);
        }

        public override IEnumerable<DeathRecord> GetAll(Expression<Func<DeathRecord, bool>> filter = null)
        {
            var query = _db.DeathRecords.Include(r => r.Person).AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.ToList();
        }
    }
}