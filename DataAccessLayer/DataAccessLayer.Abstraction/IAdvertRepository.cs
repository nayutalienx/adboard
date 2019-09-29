using DataAccessLayer.Models;
using System;

namespace DataAccessLayer.Abstraction
{
   
    public interface IAdvertRepository : IRepository<Advert>
    {
        public Advert[] GetAll(long user_id);
    }
}
