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
    public class UrnRepository : Repository<Urn>
    {
        private readonly ApplicationDbContext _db;

        public UrnRepository(ApplicationDbContext context) : base(context)
        {
            _db = context;
        }

        public override Urn Add(Urn entity)
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

        public override Urn Get(params object[] keyValues)
        {
            long key = (long)keyValues[0];
            return _db.Urns
                .Include(m => m.GraveSite)
                .Include(m => m.GraveSite.Location)
                .SingleOrDefault(m => m.GraveSiteId == key);
        }

        public override Urn Get(Expression<Func<Urn, bool>> filter)
        {
            return _db.Urns
                .Include(m => m.GraveSite)
                .Include(m => m.GraveSite.Location)
                .FirstOrDefault(filter);
        }

        public override IEnumerable<Urn> GetAll(Expression<Func<Urn, bool>> filter = null)
        {
            var query = _db.Urns
                .Include(m => m.GraveSite)
                .Include(m => m.GraveSite.Location)
                .AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.ToList();
        }

        public override void Remove(Urn entity)
        {
            var dbSite = _db.GraveSites.Find(entity.GraveSiteId);
            var dbUrn = _db.Urns.Find(entity.GraveSiteId);
            _db.Urns.Remove(dbUrn);
            _db.SaveChanges();
            _db.GraveSites.Remove(dbSite);
        }
    }
}