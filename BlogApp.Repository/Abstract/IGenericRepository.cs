using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BlogApp.Repository.Abstract
{
    public interface IGenericRepository<TEntity> where TEntity:class
    {
        TEntity Get(int id);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> expression);
        void Add(TEntity entity);
        void Remove(TEntity entity);
        void Update(TEntity entity);
        void Save();
    }
}
