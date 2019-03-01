using Assignment5.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Assignment5.Data
{
    public abstract class ReadOnlyRepository<TEntity> :
        IReadOnlyRepository<TEntity>
        where TEntity : class, IBaseEntity
    {
        protected readonly DbContext _Db;
        private DbSet<TEntity> _DbSet;

        public ReadOnlyRepository(DbContext db)
        {
            if (db == null) throw new ArgumentNullException("db");

            this._Db = db;
        }

        protected DbSet<TEntity> DbSet
        {
            get
            {
                return this._DbSet = this._DbSet ?? this._Db.Set<TEntity>();
            }
        }

        public virtual TEntity Get(int id)
        {
            return this.DbSet.Find(id);
        }

        public virtual TEntity Get(int id, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = this.GetIncludesQuery(includes);

            return this.GetIncludesQuery(includes).SingleOrDefault(e => e.Id == id);
        }

        public virtual IEnumerable<TEntity> All()
        {
            return this.DbSet.ToList();
        }

        public virtual IEnumerable<TEntity> All(params Expression<Func<TEntity, object>>[] includes)
        {
            return this.GetIncludesQuery(includes).ToList();
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> conditions)
        {
            return this.DbSet.Where(conditions).ToList();
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> conditions, params Expression<Func<TEntity, object>>[] includes)
        {
            return this.GetIncludesQuery(includes).Where(conditions).ToList();
        }

        public virtual IEnumerable<TEntity> Find<TKey>(Expression<Func<TEntity, bool>> conditions, Expression<Func<TEntity, TKey>> orderBy, OrderByDirection direction, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = this.GetIncludesQuery(includes).Where(conditions);
            var orderedQuery = direction == OrderByDirection.Ascending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);

            return orderedQuery.ToList();
        }

        public virtual IEnumerable<TEntity> Find<TKey>(Expression<Func<TEntity, bool>> conditions, Expression<Func<TEntity, TKey>> orderBy, OrderByDirection direction, int take, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = this.GetIncludesQuery(includes).Where(conditions);
            var orderedQuery = direction == OrderByDirection.Ascending ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);

            return orderedQuery.Take(take).ToList();
        }

        public virtual bool Any(Expression<Func<TEntity, bool>> conditions)
        {
            return this.DbSet.Any(conditions);
        }

        public virtual int Count()
        {
            return this.DbSet.Count();
        }

        public virtual int Count(Expression<Func<TEntity, bool>> conditions)
        {
            return this.DbSet.Where(conditions).Count();
        }

        protected IQueryable<TEntity> GetIncludesQuery(Expression<Func<TEntity, object>>[] includes)
        {
            var query = this.DbSet.AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query;
        }
    }
}