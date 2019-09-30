using DataAccessLayer.Abstraction;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.StubImplementation
{
    public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private static long id = 0;
        protected readonly List<T> Context;

        protected BaseRepository()
        {
            Context = new List<T>();
        }

        public void Add(T entity)
        {
            entity.Id = id++;
            Context.Add(entity);
        }

        public void Update(T entity)
        {
            var en = Get(entity.Id);
            en = entity;
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
