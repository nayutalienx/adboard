using AutoMapper;
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
        private readonly IMapper _mapper;

        public AdvertManager(IAdvertRepository advertRepositor, IUserRepository userRepository, IMapper mapper)
        {
            _advertRepository = advertRepositor;
            _userRepository = userRepository;
            _mapper = mapper;
            
        }

        public void Create(NewAdvertDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));
            //if (dto.Author.Id == -1)
            //    throw new Exception("Гости не могут создавать объявления");
            if (string.IsNullOrWhiteSpace(dto.Header))
                throw new Exception("Наименование обязательно");

            var advert = _mapper.Map<Advert>(dto);

            _advertRepository.Add(advert);
            _advertRepository.SaveChanges();
        }

        public AdvertDto Get(long id)
        {
            return _mapper.Map<AdvertDto>(_advertRepository.Get(id));
        }

        public AdvertDto[] GetAll()
        {
            return _mapper.Map<AdvertDto[]>(_advertRepository.GetAll());
        }

        public AdvertDto[] GetAllByUser(UserDto user)
        {
            if (user.Id == -1)
                throw new Exception("У гостей не может быть объявлений");
            return _mapper.Map<AdvertDto[]>(_advertRepository.GetAllByUser(_mapper.Map<User>(user)));
        }

        public void Remove(RemoveAdvertDto dto)
        {
            if (dto.UserId == -1)
                throw new Exception("Гости не могут удалять объявления");
            var advert = _mapper.Map<AdvertDto>(_advertRepository.Get(dto.AdvertId));
            if (advert == null)
                throw new Exception("Объявление не найдено");
            if (advert.Author.Id != dto.UserId)
                throw new Exception("Вы не имеете право удалять это объявление");
            _advertRepository.Remove(_mapper.Map<Advert>(dto));
            _advertRepository.SaveChanges();
        }

        public AdvertDto[] Search(string query)
        {
            return _mapper.Map<AdvertDto[]>(_advertRepository.GetAll()).Where(x => {
                return x.Header.Contains(query) || x.Description.Contains(query);
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

            var advert = _mapper.Map<AdvertDto>(_advertRepository.Get(dto.AdvertId));
            if (advert == null)
                throw new Exception("Объявление не найдено");

            if (advert.Author.Id != dto.UserId)
                throw new Exception("Вы не имеете право редактировать это объявление");

            //advert.Header = dto.Header;
            //advert.Description = dto.Description;
            //advert.Category = dto.Category;
            //advert.SubCategory = dto.SubCategory;
            //advert.Price = dto.Price;

            _advertRepository.Update(_mapper.Map<Advert>(dto));
            _advertRepository.SaveChanges();
        }
    }
}
