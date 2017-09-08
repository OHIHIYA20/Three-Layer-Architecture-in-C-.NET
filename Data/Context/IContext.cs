using Models;
using System;
using System.Data.Entity;

namespace Data.Context
{
    public interface IContext<T> : IDisposable where T : BaseEntity
    {
        IDbSet<T> Set<TEntity>() where TEntity : T;
        int SaveChanges();
        void SetModified(T entity);
    }
}
