using BusinessLogicLayer.Objects.Advert;
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
        void Update(UpdateAdvertDto dto);
        void Remove(RemoveAdvertDto dto);
        AdvertDto[] Search(string query);
    }
}
