using BusinessLogicLayer.Abstraction;
using BusinessLogicLayer.Objects.Advert;
using BusinessLogicLayer.Objects.User;
using DataAccessLayer.Abstraction;
using DataAccessLayer.Models;
using System;
using System.Linq;

namespace BusinessLogicLayer.Implementation
{
    public class AdvertManager : IAdvertManager
    {
        private readonly IAdvertRepository _advertRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICommentRepository _commentRepository;
        public AdvertManager(IAdvertRepository advertRepositor, IUserRepository userRepository, ICommentRepository commentRepository)
        {
            _advertRepository = advertRepositor;
            _userRepository = userRepository;
            _commentRepository = commentRepository;
        }

        public void Create(NewAdvertDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));
            if (dto.UserId == -1)
                throw new Exception("Гости не могут создавать объявления");
            if (string.IsNullOrWhiteSpace(dto.Header))
                throw new Exception("Наименование обязательно");


            var author = _userRepository.Get(dto.UserId);

            var advert = new Advert
            {
                Topic = dto.Header,
                Description = dto.Description,
                CreatedDataTime = DateTime.Now,
                Id = dto.UserId
            };

            _advertRepository.Add(advert);
        }

        public AdvertDto[] GetAll()
        {
            return _advertRepository.GetAll().Select(x => {
                var author = _userRepository.Get(x.Id);
                return new AdvertDto
                {
                    AdvertId = x.Id,
                    Header = x.Topic,
                    Description = x.Description,
                    TimeCreated = x.CreatedDataTime,
                    Author = new UserDto
                    {
                        Name = author.Name,
                        PhoneNumber = author.PhoneNumber
                    }
                };
            }).ToArray();
        }

        public AdvertDto[] GetAllByUser(long user_id)
        {
            if (user_id == -1)
                throw new Exception("У гостей не может быть объявлений");
            var author = _userRepository.Get(user_id);
            return _advertRepository.GetAll(user_id).Select(x => {
                return new AdvertDto
                {
                    AdvertId = x.Id,
                    Header = x.Topic,
                    Description = x.Description,
                    TimeCreated = x.CreatedDataTime,
                    Author = new UserDto
                    {
                        Name = author.Name,
                        PhoneNumber = author.PhoneNumber
                    }
                };
            }).ToArray();
        }

        public void Remove(RemoveAdvertDto dto)
        {
            if (dto.UserId == -1)
                throw new Exception("Гости не могут удалять объявления");
            var advert = _advertRepository.Get(dto.AdvertId);
            if (advert == null)
                throw new Exception("Объявление не найдено");
            if (advert.Author.Id != dto.UserId)
                throw new Exception("Вы не имеете право удалять это объявление");
            _advertRepository.Remove(advert);
        }

        public AdvertDto[] Search(string query)
        {
            return _advertRepository.GetAll().Where(x => {
                return x.Topic.Contains(query) || x.Description.Contains(query);
            }).Select(x => {
                var author = _userRepository.Get(x.Author.Id);
                return new AdvertDto
                {
                    AdvertId = x.Id,
                    Header = x.Topic,
                    Description = x.Description,
                    TimeCreated = x.CreatedDataTime,
                    Author = new UserDto
                    {
                        Name = author.Name,
                        PhoneNumber = author.PhoneNumber
                    }
                };
            }).ToArray();
        }

        public void Update(UpdateAdvertDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));
            if (dto.UserId == -1)
                throw new Exception("Гости не могут обновлять объявления");
            if (string.IsNullOrWhiteSpace(dto.Header))
                throw new Exception("Наименование обязательно");

            var advert = _advertRepository.Get(dto.AdvertId);
            if (advert == null)
                throw new Exception("Объявление не найдено");

            if (advert.Author.Id != dto.UserId)
                throw new Exception("Вы не имеете право редактировать это объявление");

            advert.Topic = dto.Header;
            advert.Description = dto.Description;

            _advertRepository.Update(advert);
        }
    }
}
