
using Adboard.Contracts.DTOs.Advert;
using Adboard.Contracts.DTOs.Comment;
using Adboard.Contracts.DTOs.Paging;
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
