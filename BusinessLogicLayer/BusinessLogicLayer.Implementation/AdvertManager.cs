using AutoMapper;
using BusinessLogicLayer.Abstraction;
using BusinessLogicLayer.Objects.Advert;
using BusinessLogicLayer.Objects.Category;
using BusinessLogicLayer.Objects.Comment;
using BusinessLogicLayer.Objects.Paging;

using DataAccessLayer.Abstraction;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        

        public void AddComment(NewCommentDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));
            if(string.IsNullOrEmpty(dto.Text))
                throw new Exception($"{nameof(dto)} Заполните все данные");

            var advert = _advertRepository.Get(dto.AdvertId);
            
            if(advert == null)
                throw new Exception($"{nameof(advert)} Такого объявления не существует");
            advert.Comments.Add(_mapper.Map<Comment>(dto));
            // _advertRepository.Update(advert);
            _advertRepository.SaveChanges();
        }

        public void Create(NewAdvertDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));
            if (string.IsNullOrWhiteSpace(dto.Header) || string.IsNullOrWhiteSpace(dto.Description))
                throw new Exception($"{nameof(dto)} Заполните все данные");
            var advert = _mapper.Map<Advert>(dto);
            _advertRepository.Add(advert);
            _advertRepository.SaveChanges();
        }

        public AdvertDto Get(long id)
        {
            return _mapper.Map<AdvertDto>(_advertRepository.Get(id));
        }

        public PagingResult<AdvertDto> GetAdvertsByFilter(AdvertFilter filter)
        {
            var adverts = _advertRepository.GetAll();
            
            if (filter == null)
                throw new NullReferenceException("Не задан фильтр");

            if (filter.AdvertId != null)
                adverts = adverts.Where(x => x.Id == filter.AdvertId);

            if (filter.UserId != null)
                adverts = adverts.Where(x => x.UserId.Equals(filter.UserId));

            if (filter.HasPhotoOnly != null && filter.HasPhotoOnly == true)
                adverts = adverts.Where(x => x.Photos != null);

            if (!string.IsNullOrEmpty(filter.Header))
                adverts = adverts.Where(x => EF.Functions.Like(x.Header, $"%{filter.Header}%"));

            if (filter.CategoryId != null)
                adverts = adverts.Where(x => x.CategoryId == filter.CategoryId);

            if (!string.IsNullOrEmpty(filter.Description))
                adverts = adverts.Where(x => EF.Functions.Like(x.Description, $"%{filter.Description}%"));

            if (filter.CreatedDateTime != null)
                adverts = adverts.Where(x => x.CreatedDateTime > filter.CreatedDateTime.From && x.CreatedDateTime < filter.CreatedDateTime.To);

            if (filter.Price != null)
                adverts = adverts.Where(x => x.Price > filter.Price.From && x.Price < filter.Price.To);

            

            adverts.Take(filter.Size).Skip((filter.CurrentPage - 1) * filter.Size);

            

            return new PagingResult<AdvertDto> {
                Items = _mapper.Map<List<AdvertDto>>(adverts),
                TotalRows = adverts.Count()

            };
        }

        public AdvertDto[] GetAll()
        {
            return _mapper.Map<AdvertDto[]>(_advertRepository.GetAll().ToArray());
        }

        public AdvertDto[] GetAllByUserId(string userid)
        {
            return _mapper.Map<AdvertDto[]>(_advertRepository.GetAll().Where(ad => ad.UserId.Equals(userid)).ToArray());
        }

        

        public void Remove(RemoveAdvertDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));
            
            
            
            var _ad = _advertRepository.Get(dto.AdvertId);
            
            if (_ad == null)
                throw new Exception($"{nameof(dto)} Такого объявления не существует");
            
            if (!_ad.UserId.Equals(dto.UserId))
                throw new Exception($"{nameof(dto)} Вы не имеете право удалять это объявление");
            
            _advertRepository.Remove(_ad);
            _advertRepository.SaveChanges();

        }

       

        public void Update(UpdateAdvertDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));
            if (string.IsNullOrWhiteSpace(dto.Header) || string.IsNullOrWhiteSpace(dto.Description))
                throw new Exception($"{nameof(dto)} Заполните все данные");
            var _ad = _advertRepository.Get(dto.Id);
            if (_ad == null)
                throw new Exception($"{nameof(dto)} Такого объявления не существует");
            if (!_ad.UserId.Equals(dto.UserId))
                throw new Exception($"{nameof(dto)} Вы не имеете право изменять это объявление");

            _ad.Header = dto.Header;
            _ad.Description = dto.Description;
            _ad.CategoryId = dto.CategoryId;
            _ad.Price = dto.Price;

            _ad.Location.Country = dto.Location.Country;
            _ad.Location.Area = dto.Location.Area;
            _ad.Location.City = dto.Location.City;
            _ad.Location.Street = dto.Location.Street;
            _ad.Location.HouseNumber = dto.Location.HouseNumber; 
            _advertRepository.SaveChanges();



        }
    }
}
