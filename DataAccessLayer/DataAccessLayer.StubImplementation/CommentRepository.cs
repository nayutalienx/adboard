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
            throw new NotImplementedException();
        }

        public void RemoveCommentsByAdvert(long id)
        {
            throw new NotImplementedException();
        }
    }
}
