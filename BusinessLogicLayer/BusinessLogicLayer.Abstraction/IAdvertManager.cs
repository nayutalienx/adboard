using BusinessLogicLayer.Objects.Advert;
using System;

namespace BusinessLogicLayer.Abstraction
{
    public interface IAdvertManager
    {
        void Create(NewAdvertDto dto);
        AdvertDto[] GetAll();
        AdvertDto[] GetAllByUser(long user_id);
        void Update(UpdateAdvertDto dto);
        void Remove(RemoveAdvertDto dto);
        AdvertDto[] Search(string query);
    }
}
