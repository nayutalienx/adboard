using DataAccessLayer.Abstraction;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly AdboardContext Context;
        protected readonly DbSet<T> Entity;
        public BaseRepository(AdboardContext context) {
            Context = context;
            Entity = Context.Set<T>();
        }
        public void Add(T entity)
        {
            Entity.Add(entity);
        }

        public T Get(long id)
        {
            var result = Entity.Find(id);
            Context.Entry(result).State = EntityState.Detached;
            return result;
        }

        public T[] GetAll()
        {
            return Entity.ToArray();
        }

        public void Remove(T entity)
        {
            Entity.Attach(entity);
            Entity.Remove(entity);
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public void Update(T entity)
        {
            Entity.Update(entity);
        }
    }
}
