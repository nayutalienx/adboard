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
        AdvertDto Get(long id);
        AdvertDto[] GetAllByUser(UserDto user);
        void AddCategory(CategoryDto dto);
        CategoryDto[] GetAllCategories();
        void Update(UpdateAdvertDto dto);
        void Remove(RemoveAdvertDto dto);
        PagingResult<AdvertDto> GetAdvertsByFilter(AdvertFilter filter);
        void AddComment(NewCommentDto dto);
    }
}
