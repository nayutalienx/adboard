using DataAccessLayer.Abstraction;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.StubImplementation
{
    public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly List<T> Context;

        protected BaseRepository()
        {
            Context = new List<T>();
        }

        public void Add(T entity)
        {
            var random = new Random();
            entity.Id = random.Next(1, 1000);
            Context.Add(entity);
        }

        public void Update(T entity)
        {
            Context.Add(entity);
        }

        public T Get(long id)
        {
            return Context.Find(x => x.Id == id);
        }

        public T[] GetAll()
        {
            return Context.ToArray();
        }

        public void Remove(T entity)
        {
            Context.Remove(entity);
        }
    }
}
