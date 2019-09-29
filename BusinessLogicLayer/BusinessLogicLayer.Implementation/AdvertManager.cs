using BusinessLogicLayer.Abstraction;
using BusinessLogicLayer.Objects.Advert;
using System;

namespace BusinessLogicLayer.Implementation
{
    public class AdvertManager : IAdvertManager
    {
        //private readonly IAdvertRepository _advertRepository;
        //private readonly IUserRepository _userRepository;
        //private readonly ICommentRepository _commentRepository;
        public void Create(NewAdvertDto dto)
        {
            throw new NotImplementedException();
        }

        public AdvertDto[] GetAll()
        {
            throw new NotImplementedException();
        }

        public AdvertDto[] GetAllByUser(long user_id)
        {
            throw new NotImplementedException();
        }

        public void Remove(RemoveAdvertDto dto)
        {
            throw new NotImplementedException();
        }

        public AdvertDto[] Search(string query)
        {
            throw new NotImplementedException();
        }

        public void Update(UpdateAdvertDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
