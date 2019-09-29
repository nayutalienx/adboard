using ConsoleApp17.DataAccessLayer.Abstraction;
using ConsoleApp17.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp17.DataAccessLayer.StubImplementation
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
    }
}
