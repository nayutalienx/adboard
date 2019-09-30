using DataAccessLayer.Abstraction;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.StubImplementation
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public Comment[] GetCommentsByAdvert(long id)
        {
            return Context.FindAll(x => { return x.AdvertId == id; }).ToArray();
        }

        public void RemoveCommentsByAdvert(long id)
        {
            Context.RemoveAll(x => {
                return x.AdvertId == id;
            });
        }
    }
}
