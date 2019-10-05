using BusinessLogicLayer.Objects.Advert;
using BusinessLogicLayer.Objects.Comment;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Abstraction
{
    public interface ICommentManager
    {
        void AddComment(NewCommentDto dto);
        CommentDto[] GetCommentsByAdvert(AdvertDto dto);
        void RemoveCommentsByAdvert(RemoveAdvertDto dto);
    }
}
