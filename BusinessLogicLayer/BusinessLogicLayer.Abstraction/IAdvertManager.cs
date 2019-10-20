using BusinessLogicLayer.Objects.Advert;
using BusinessLogicLayer.Objects.Category;
using BusinessLogicLayer.Objects.Comment;
using BusinessLogicLayer.Objects.Paging;
using BusinessLogicLayer.Objects.User;
using System;

namespace BusinessLogicLayer.Abstraction
{
    public interface IAdvertManager
    {
        void Create(NewAdvertDto dto);
        AdvertDto[] GetAll();
        void Update(UpdateAdvertDto dto);
        void Remove(RemoveAdvertDto dto);
        PagingResult<AdvertDto> GetAdvertsByFilter(AdvertFilter filter);
        void AddComment(NewCommentDto dto);
    }
}
