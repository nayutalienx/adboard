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
        
        private readonly IMapper _mapper;

        public AdvertManager(IAdvertRepository advertRepositor, IMapper mapper)
        {
            _advertRepository = advertRepositor;
            
            _mapper = mapper;
            
        }

        public void Create(NewAdvertDto dto)
        {
            if (dto.AuthorId == -1)
                throw new Exception($"{nameof(dto)} Гости не могут добавлять объявления");
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));
            if (string.IsNullOrWhiteSpace(dto.Header) || string.IsNullOrWhiteSpace(dto.Description) || string.IsNullOrWhiteSpace(dto.Category) || string.IsNullOrWhiteSpace(dto.SubCategory))
                throw new Exception($"{nameof(dto)} Заполните все данные");
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
                throw new Exception($"{nameof(user)} У гостей не может быть объявлений");
            return _mapper.Map<AdvertDto[]>(_advertRepository.GetAllByUser(_mapper.Map<User>(user)));
        }

        public void Remove(RemoveAdvertDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));
            if (dto.UserId == -1)
                throw new Exception($"{nameof(dto)} Гости не могут удалять объявления");
            var _ad = _advertRepository.Get(dto.AdvertId);
            if (_ad == null)
                throw new Exception($"{nameof(dto)} Такого объявления не существует");
            if (_ad.AuthorId != dto.UserId)
                throw new Exception($"{nameof(dto)} Вы не имеете право удалять это объявление");
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
                throw new Exception($"{nameof(dto)} Гости не могут обновлять объявления");
            if (string.IsNullOrWhiteSpace(dto.Header) || string.IsNullOrWhiteSpace(dto.Description) || string.IsNullOrWhiteSpace(dto.Category) || string.IsNullOrWhiteSpace(dto.SubCategory))
                throw new Exception($"{nameof(dto)} Заполните все данные");
            var _ad = _advertRepository.Get(dto.AdvertId);
            if (_ad == null)
                throw new Exception($"{nameof(dto)} Такого объявления не существует");
            if (_ad.AuthorId != dto.UserId)
                throw new Exception($"{nameof(dto)} Вы не имеете право изменять это объявление");
            _advertRepository.Update(_mapper.Map<Advert>(dto));
            _advertRepository.SaveChanges();
        }
    }
}
