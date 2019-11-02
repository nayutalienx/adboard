using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLayer.Abstraction
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();

        T Get(long id);

        void Add(T entity);

        void Update(T entity);

        void Remove(T entity);

        void SaveChanges();

    }
}
