using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;

namespace Persistance.Repositories
{
    public class ManagerRepository : Repository<Manager>
    {
        private readonly ApplicationDbContext _db;

        public ManagerRepository(ApplicationDbContext context) : base(context)
        {
            _db = context;
        }

        public override Manager Add(Manager entity)
        {
            var worker = entity.Worker;
            if (!_db.Workers.Any(w => w.PersonId == worker.PersonId))
            {
                worker = _db.Workers.Add(worker);
                _db.SaveChanges();
            }
            entity.WorkerId = worker.PersonId;
            return base.Add(entity);
        }

        public override Manager Get(params object[] keyValues)
        {
            long key = (long)keyValues[0];
            return _db.Managers
                .Include(m => m.Worker)
                .Include(m => m.Employees)
                .SingleOrDefault(m => m.WorkerId == key);
        }

        public override Manager Get(Expression<Func<Manager, bool>> filter)
        {
            return _db.Managers
                .Include(m => m.Worker)
                .Include(m => m.Employees)
                .FirstOrDefault(filter);
        }

        public override IEnumerable<Manager> GetAll(Expression<Func<Manager, bool>> filter = null)
        {
            var query = _db.Managers
                .Include(m => m.Worker)
                .Include(m => m.Employees)
                .AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.ToList();
        }
    }
}