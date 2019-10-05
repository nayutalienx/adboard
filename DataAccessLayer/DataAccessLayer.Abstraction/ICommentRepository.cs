using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Abstraction
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Comment[] GetCommentsByAdvert(Advert advert);
        void RemoveCommentsByAdvert(Advert advert);
    }
}
