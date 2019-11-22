using Adboard.Contracts.DTOs.Advert;
using Adboard.Contracts.DTOs.Comment;
using Adboard.Contracts.DTOs.Paging;
using AutoMapper;
using BusinessLogicLayer.Abstraction;


using DataAccessLayer.Abstraction;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Implementation
{
    public class AdvertManager : IAdvertManager
    {
        private readonly IAdvertRepository _advertRepository;
        private readonly IPhotoRepository _photoRepository;
        
        
        private readonly IMapper _mapper;

        public AdvertManager(IAdvertRepository advertRepositor, IPhotoRepository photoRepository, IMapper mapper)
        {
            _advertRepository = advertRepositor;
            _photoRepository = photoRepository;
            _mapper = mapper;
        }

        

        public async Task<CommentDto> AddCommentAsync(NewCommentDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));
            if(string.IsNullOrEmpty(dto.Text))
                throw new Exception($"{nameof(dto)} Заполните все данные");

            var advert = await _advertRepository.GetAsync(dto.AdvertId);
            
            if(advert == null)
                throw new Exception($"{nameof(advert)} Такого объявления не существует");
            var entity = _mapper.Map<Comment>(dto);
            advert.Comments.Add(entity);
            await _advertRepository.SaveChangesAsync();
            return _mapper.Map<CommentDto>(entity);
        }

        public async Task<AdvertDto> CreateAsync(NewAdvertDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));
            
            if (string.IsNullOrWhiteSpace(dto.Header) || string.IsNullOrWhiteSpace(dto.Description))
                throw new Exception($"{nameof(dto)} Заполните все данные");

            if (dto.Header.Length > 30)
                throw new Exception($"{nameof(dto)} Заголовок не более 30 символов");

            if (dto.Description.Length > 650)
                throw new Exception($"{nameof(dto)} Описание не более 650 символов");
            
            var advert = _mapper.Map<Advert>(dto);
            var result = await _advertRepository.AddAsync(advert);
            try
            {
                await _advertRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                string mes = ex.Message;
                throw new Exception(ex.Message);
            }
            return _mapper.Map<AdvertDto>(result);
        }

        public async Task<AdvertDto> GetAsync(long id)
        {
            return _mapper.Map<AdvertDto>(await _advertRepository.GetAsync(id));
        }

        

        public async Task<IReadOnlyCollection<AdvertDto>> GetAdvertsByFilterAsync(AdvertFilter filter)
        {
            var adverts = await _advertRepository.GetAllAsync();
            
            if (filter == null)
                throw new NullReferenceException("Не задан фильтр");

            

            if (filter.HasPhotoOnly != null && filter.HasPhotoOnly == true)
                adverts = adverts.Where(x => x.Photos.Count > 0);

            if (!string.IsNullOrEmpty(filter.Header))
                adverts = adverts.Where(x => EF.Functions.Like(x.Header, $"%{filter.Header}%"));

            if (filter.CategoryId != null)
                adverts = adverts.Where(x => x.CategoryId == filter.CategoryId || x.Category.ParentCategory.Id == filter.CategoryId);

            if (filter.Region != null)
                adverts = adverts.Where(x => EF.Functions.Like(x.Location.Country, $"%{filter.Region}%") || EF.Functions.Like(x.Location.Area, $"%{filter.Region}%") ||
                EF.Functions.Like(x.Location.City, $"%{filter.Region}%") || EF.Functions.Like(x.Location.Street, $"%{filter.Region}%"));
                
            if (!string.IsNullOrEmpty(filter.Description))
                adverts = adverts.Where(x => EF.Functions.Like(x.Description, $"%{filter.Description}%"));

            if (filter.CreatedDateTime != null)
                adverts = adverts.Where(x => x.CreatedDateTime > filter.CreatedDateTime.From && x.CreatedDateTime < filter.CreatedDateTime.To);

            if (filter.Price != null)
                adverts = adverts.Where(x => x.Price > filter.Price.From && x.Price < filter.Price.To);

            if (filter.UserId != null)
            {
                adverts = adverts.Where(x => x.UserId.Equals(filter.UserId));
            }
           

            if (filter.AdvertId != null)
            {
                adverts = adverts.Where(x => x.Id == filter.AdvertId);
            }
            else
            {
                adverts = adverts.Select(x => new Advert
                {
                    Photos = new List<Photo> { x.Photos.FirstOrDefault() },
                    Id = x.Id,
                    Header = x.Header,
                    CreatedDateTime = x.CreatedDateTime,
                    Category = x.Category,
                    Price = x.Price
                });
            }

            

            adverts = adverts.Skip((filter.CurrentPage - 1) * filter.Size).Take(filter.Size);

            return _mapper.Map<List<AdvertDto>>(adverts.ToArray());
        }



        public async Task<IReadOnlyCollection<AdvertDto>> GetAllAsync()
        {
            return _mapper.Map<IReadOnlyCollection<AdvertDto>>(await _advertRepository.GetAllAsync());
        }

        public async Task<IReadOnlyCollection<AdvertDto>> GetAllByUserIdAsync(string userid)
        {
            var list = await _advertRepository.GetAllAsync();
            return _mapper.Map<IReadOnlyCollection<AdvertDto>>(list.Where(ad => ad.UserId.Equals(userid)));
        }

        

        public async Task RemoveAsync(RemoveAdvertDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));
            
            
            
            var _ad = await _advertRepository.GetAsync(dto.AdvertId);
            
            if (_ad == null)
                throw new Exception($"{nameof(dto)} Такого объявления не существует");
           
            
            await _advertRepository.RemoveAsync(_ad);
            await _advertRepository.SaveChangesAsync();

        }

       

        public async Task<AdvertDto> UpdateAsync(UpdateAdvertDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));
            if (string.IsNullOrWhiteSpace(dto.Header))
                throw new Exception($"{nameof(dto)} Заполните все данные");
            var _ad = await _advertRepository.GetAsync(dto.Id);
            if (_ad == null)
                throw new Exception($"{nameof(dto)} Такого объявления не существует");
            if (!_ad.UserId.Equals(dto.UserId))
                throw new Exception($"{nameof(dto)} Вы не имеете право изменять это объявление");
            if (dto.Description?.Length > 650)
                throw new Exception($"{nameof(dto)} Описание не более 650 символов");

            _ad.Header = dto.Header;
            if(dto.Description != null)
                _ad.Description = dto.Description;

            if(dto.CategoryId != 0)
                _ad.CategoryId = dto.CategoryId;
            _ad.Price = dto.Price;

            _ad.Location.Country = dto.Location.Country;
            _ad.Location.Area = dto.Location.Area;
            _ad.Location.City = dto.Location.City;
            _ad.Location.Street = dto.Location.Street;
            _ad.Location.HouseNumber = dto.Location.HouseNumber;

            ICollection<Photo> photos = new List<Photo>();

            if (dto.Photo?.Length > 0) {

                if (_ad.Photos?.Count > 0) { 
                    foreach(Photo ph in _ad.Photos)
                        photos.Add(new Photo
                        {
                            Id = ph.Id,
                            Advert = ph.Advert,
                            AdvertId = ph.AdvertId,
                            Data = ph.Data
                        });
                }
                _ad.Photos = _mapper.Map<ICollection<Photo>>(dto.Photo);
                await _advertRepository.UpdateAsync(_ad);
            }
                
            await _advertRepository.SaveChangesAsync();
            try
            {
                foreach (Photo ph in photos)
                    await _photoRepository.RemoveAsync(ph);
                await _photoRepository.SaveChangesAsync();
            }
            catch (Exception ex) { }
            return _mapper.Map<AdvertDto>(_ad);
        }
    }
}
