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
        
        
        private readonly IMapper _mapper;

        public AdvertManager(IAdvertRepository advertRepositor, IMapper mapper)
        {
            _advertRepository = advertRepositor;
           
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
            
            var advert = _mapper.Map<Advert>(dto);
            var result = await _advertRepository.AddAsync(advert);
            await _advertRepository.SaveChangesAsync();
            return _mapper.Map<AdvertDto>(result);
        }

        public async Task<AdvertDto> GetAsync(long id)
        {
            return _mapper.Map<AdvertDto>(await _advertRepository.GetAsync(id));
        }

        public async Task<PagingResult<AdvertDto>> GetAdvertsByFilterAsync(AdvertFilter filter)
        {
            var adverts = await _advertRepository.GetAllAsync();
            
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
            
            if (!_ad.UserId.Equals(dto.UserId))
                throw new Exception($"{nameof(dto)} Вы не имеете право удалять это объявление");
            
            await _advertRepository.RemoveAsync(_ad);
            await _advertRepository.SaveChangesAsync();

        }

       

        public async Task<AdvertDto> UpdateAsync(UpdateAdvertDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));
            if (string.IsNullOrWhiteSpace(dto.Header) || string.IsNullOrWhiteSpace(dto.Description))
                throw new Exception($"{nameof(dto)} Заполните все данные");
            var _ad = await _advertRepository.GetAsync(dto.Id);
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
            await _advertRepository.SaveChangesAsync();
            return _mapper.Map<AdvertDto>(_ad);



        }
    }
}
