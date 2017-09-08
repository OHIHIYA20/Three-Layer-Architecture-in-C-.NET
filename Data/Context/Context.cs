using Models;
using System.Data.Entity;

namespace Data.Context
{
    public class Context<T> : TestEntities, IContext<T> where T : BaseEntity
    {
        public void SetModified(T entity)
        {
            var entry = Entry(entity);
            entry.State = EntityState.Modified;
        }

        IDbSet<T> IContext<T>.Set<TEntity>()
        {
            return Set<T>();
        }

        public Context()
        {

        }

    }
}
