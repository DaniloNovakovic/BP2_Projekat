using Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace Persistance.Repositories
{
    public class ChapelRepository : Repository<Chapel>
    {
        private readonly ApplicationDbContext _db;

        public ChapelRepository(ApplicationDbContext context) : base(context)
        {
            _db = context;
        }

        public override IEnumerable<Chapel> GetAll(Expression<Func<Chapel, bool>> filter = null)
        {
            var query = _db.Chapels.Include(c => c.Location).AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.ToList();
        }

        public override Chapel Get(Expression<Func<Chapel, bool>> filter)
        {
            return _db.Chapels.Include(c => c.Location).FirstOrDefault(filter);
        }

        public override Chapel Get(params object[] keyValues)
        {
            long key = (long)keyValues[0];

            //var id = new SqlParameter("@id", key);
            //return _db.Database.SqlQuery<Chapel>("dbo.SelectChapelById @id", id).SingleOrDefault();

            return _db.Chapels.Include(c => c.Location).Single(c => c.Id == key);
        }

        public override void Remove(Chapel entity)
        {
            var location = entity.Location;
            _db.Chapels.Remove(entity);
            _db.Locations.Remove(location);
        }
    }
}