using DataAccessLayer.Abstraction;
using DataAccessLayer.Models;
using System;

namespace DataAccessLayer.StubImplementation
{
    public class AdvertRepository : BaseRepository<Advert>, IAdvertRepository
    {
        public Advert[] GetAll(long user_id)
        {
            return Context.FindAll(x => { return x.UserId == user_id; }).ToArray();
        }
    }
}
