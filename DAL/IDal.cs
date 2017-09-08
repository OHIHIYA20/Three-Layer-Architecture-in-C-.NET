using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace DAL
{
    public interface IDAL<T> : IDisposable where T : BaseEntity
    {
        T GetById(long id);
        IQueryable<T> GetAll();
        IQueryable<T> SelectAll();
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);

        bool Insert(T entity);
        void InsertAll(IEnumerable<T> entities);

        bool Update(T entity);
        void Upsert(T entity);

        bool Delete(T entity);
        bool Delete(Expression<Func<T, bool>> predicate);
        bool DeleteAll();
        bool DeleteById(long id);
        void Refresh();
    }
}
