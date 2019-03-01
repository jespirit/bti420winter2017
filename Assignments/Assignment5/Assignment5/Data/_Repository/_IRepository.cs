using Assignment5.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Assignment5.Data
{
    public interface IRepository<TEntity>
        where TEntity : class, IBaseEntity
    {
        TEntity Get(int id);
        TEntity Get(int id, params Expression<Func<TEntity, object>>[] includes);
        IEnumerable<TEntity> All();
        IEnumerable<TEntity> All(params Expression<Func<TEntity, object>>[] includes);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> conditions);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> conditions, params Expression<Func<TEntity, object>>[] includes);
        IEnumerable<TEntity> Find<TKey>(Expression<Func<TEntity, bool>> conditions, Expression<Func<TEntity, TKey>> orderBy, OrderByDirection direction, params Expression<Func<TEntity, object>>[] includes);
        IEnumerable<TEntity> Find<TKey>(Expression<Func<TEntity, bool>> conditions, Expression<Func<TEntity, TKey>> orderBy, OrderByDirection direction, int take, params Expression<Func<TEntity, object>>[] includes);
        bool Any(Expression<Func<TEntity, bool>> conditions);
        int Count();
        int Count(Expression<Func<TEntity, bool>> conditions);
        void Detach(TEntity entity);
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Remove(TEntity entity);
    }
}