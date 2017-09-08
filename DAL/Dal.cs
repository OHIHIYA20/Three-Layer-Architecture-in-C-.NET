using Data.Context;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Models;

namespace DAL
{
    public class DAL<T> : IDAL<T> where T : BaseEntity
    {   
        public IContext<T> Dal { get; set; }

        public DAL()
        {
            Dal = new Context<T>();
        }

        protected IDbSet<T> Context
        {
            get { return Dal.Set<T>(); }
        }

        public void Refresh()
        {
            Dal = new Context<T>();
        }

        public virtual T GetById(long id)
        {
            return Context.Find(id);
        }

        public virtual IQueryable<T> GetAll()
        {
            return Context.AsQueryable();
        }

        public IQueryable<T> SelectAll()
        {
            return Context;
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return Context.Where(predicate);
        }

        public virtual bool Delete(T entity)
        {
            Context.Remove(entity);
            return ApplyChanges();
        }

        public bool Delete(Expression<Func<T, bool>> predicate)
        {
            var entities = Get(predicate).ToList();
            entities.ForEach(e => Context.Remove(e));
            return ApplyChanges();
        }

        public virtual bool DeleteById(long id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                Context.Remove(entity);
                return ApplyChanges();
            }
            return false;
        }

        public virtual bool DeleteAll()
        {
            foreach (var entity in Context)
            {
                Context.Remove(entity);
            }
            return ApplyChanges();
        }

        public virtual bool Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            Context.Add(entity);

            var cnt = Dal.SaveChanges();
            return cnt == 1;

        }

        public virtual void InsertAll(IEnumerable<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }
            foreach (var entity in entities)
            {
                Context.Add(entity);
            }
            Dal.SaveChanges();
        }

        public virtual bool Update(T entity)
        {
            var cnt = 0;
            if (entity != null)
            {
                if (!Context.Local.Contains(entity))
                {
                    Context.Attach(entity);
                }
                Dal.SetModified(entity);
                cnt = Dal.SaveChanges();
            }
            return cnt == 1;
        }


        public virtual void Upsert(T entity)
        {
            var item = Context.Find(entity);
            if (item == null)
            {
                Context.Add(entity);
            }
            Dal.SaveChanges();
        }

        protected bool ApplyChanges()
        {
            Dal.SaveChanges();
            return true;
        }

        public void Dispose()
        {
            if (Dal != null)
            {
                Dal.Dispose();
            }
        }
    }
}
