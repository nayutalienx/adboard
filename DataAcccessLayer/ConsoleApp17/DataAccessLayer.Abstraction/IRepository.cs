using ConsoleApp17.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp17.DataAccessLayer.Abstraction
{
    public interface IRepository<T> where T : BaseEntity
    {
        T[] GetAll();

        T Get(long id);

        void Add(T entity);

        void Update(T entity);

        void Remove(T entity);

    }
}
