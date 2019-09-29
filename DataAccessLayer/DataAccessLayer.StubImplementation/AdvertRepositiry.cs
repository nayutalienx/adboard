using DataAccessLayer.Abstraction;
using DataAccessLayer.Models;
using System;

namespace DataAccessLayer.StubImplementation
{
    public class AdvertRepositiry : BaseRepository<Advert>, IAdvertRepository
    {
        public Advert[] GetAll(long user_id)
        {
            throw new NotImplementedException();
        }
    }
}
