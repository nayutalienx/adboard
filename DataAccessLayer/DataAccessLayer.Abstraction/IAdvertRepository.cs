﻿using DataAccessLayer.Models;
using System;
using System.Linq;

namespace DataAccessLayer.Abstraction
{
   
    public interface IAdvertRepository : IRepository<Advert>
    {
        public IQueryable<Advert> GetAllByUser(User user);
    }
}
