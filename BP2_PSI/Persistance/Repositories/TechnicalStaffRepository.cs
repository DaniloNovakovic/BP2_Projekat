﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Persistance.Repositories
{
    public class TechnicalStaffRepository : Repository<TechnicalStaff>
    {
        private readonly ApplicationDbContext _db;

        public TechnicalStaffRepository(ApplicationDbContext context) : base(context)
        {
            _db = context;
        }

        public override TechnicalStaff Add(TechnicalStaff entity)
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

        public override TechnicalStaff Get(params object[] keyValues)
        {
            long key = (long)keyValues[0];
            return _db.TechnicalStaff
                .Include(m => m.Worker)
                .Include(m => m.Manager)
                .SingleOrDefault(m => m.WorkerId == key);
        }

        public override TechnicalStaff Get(Expression<Func<TechnicalStaff, bool>> filter)
        {
            return _db.TechnicalStaff
                .Include(m => m.Worker)
                .Include(m => m.Manager)
                .FirstOrDefault(filter);
        }

        public override IEnumerable<TechnicalStaff> GetAll(Expression<Func<TechnicalStaff, bool>> filter = null)
        {
            var query = _db.TechnicalStaff
                .Include(m => m.Worker)
                .Include(m => m.Manager)
                .AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.ToList();
        }

        public override void Remove(TechnicalStaff entity)
        {
            var dbWorker = _db.Workers.Find(entity.WorkerId);
            var dbStaff = _db.TechnicalStaff.Find(entity.WorkerId);
            _db.TechnicalStaff.Remove(dbStaff);
            _db.SaveChanges();
            _db.Workers.Remove(dbWorker);
        }
    }
}