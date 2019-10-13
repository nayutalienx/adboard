using DataAccessLayer.Abstraction;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLayer.Repositories
{
    public class AdvertRepository : BaseRepository<Advert>, IAdvertRepository
    {
        public AdvertRepository(AdboardContext context) : base(context)
        {
        }


        public IQueryable<Advert> GetAllByUser(User user)
        {
            return Entity.Where(ad => ad.Author.Id == user.Id);
           
        }
    }
}
