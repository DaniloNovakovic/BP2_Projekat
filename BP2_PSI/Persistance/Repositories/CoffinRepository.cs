using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;

namespace Persistance.Repositories
{
    public class CoffinRepository : Repository<Coffin>
    {
        private readonly ApplicationDbContext _db;

        public CoffinRepository(ApplicationDbContext context) : base(context)
        {
            _db = context;
        }

        public override Coffin Add(Coffin entity)
        {
            var graveSite = _db.GraveSites.FirstOrDefault(gs => gs.DeathRecordId == entity.GraveSite.DeathRecordId);
            if (graveSite == null)
            {
                graveSite = _db.GraveSites.Add(entity.GraveSite);
                _db.SaveChanges();
            }
            entity.GraveSiteId = graveSite.DeathRecordId;
            return base.Add(entity);
        }

        public override Coffin Get(params object[] keyValues)
        {
            long key = (long)keyValues[0];
            return _db.Coffins
                .Include(m => m.GraveSite)
                .Include(m => m.GraveSite.Location)
                .SingleOrDefault(m => m.GraveSiteId == key);
        }

        public override Coffin Get(Expression<Func<Coffin, bool>> filter)
        {
            return _db.Coffins
                .Include(m => m.GraveSite)
                .Include(m => m.GraveSite.Location)
                .FirstOrDefault(filter);
        }

        public override IEnumerable<Coffin> GetAll(Expression<Func<Coffin, bool>> filter = null)
        {
            var query = _db.Coffins
                .Include(m => m.GraveSite)
                .Include(m => m.GraveSite.Location)
                .AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.ToList();
        }

        public override void Remove(Coffin entity)
        {
            var dbSite = _db.GraveSites.Find(entity.GraveSiteId);
            var dbCoffin = _db.Coffins.Find(entity.GraveSiteId);
            _db.Coffins.Remove(dbCoffin);
            _db.SaveChanges();
            _db.GraveSites.Remove(dbSite);
        }
    }
}