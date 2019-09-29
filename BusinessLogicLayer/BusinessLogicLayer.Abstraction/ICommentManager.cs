using BusinessLogicLayer.Objects.Comment;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Abstraction
{
    public interface ICommentManager
    {
        void AddComment(NewCommentDto dto);
        CommentDto[] GetCommentsByAdvert(long id);
        void RemoveCommentsByAdvert(long id);
    }
}
