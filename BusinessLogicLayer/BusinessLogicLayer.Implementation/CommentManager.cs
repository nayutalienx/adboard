using BusinessLogicLayer.Abstraction;
using BusinessLogicLayer.Objects.Comment;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Implementation
{
    public class CommentManager : ICommentManager
    {
        public void AddComment(NewCommentDto dto)
        {
            throw new NotImplementedException();
        }

        public CommentDto[] GetCommentsByAdvert(long id)
        {
            throw new NotImplementedException();
        }

        public void RemoveCommentsByAdvert(long id)
        {
            throw new NotImplementedException();
        }
    }
}
