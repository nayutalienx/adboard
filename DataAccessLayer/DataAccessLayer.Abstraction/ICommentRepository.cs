using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Abstraction
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Comment[] GetCommentsByAdvert(long id);
        void RemoveCommentsByAdvert(long id);
    }
}
