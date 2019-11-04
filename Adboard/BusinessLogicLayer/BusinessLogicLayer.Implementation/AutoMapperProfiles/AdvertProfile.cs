using Adboard.Contracts.DTOs.Advert;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Implementation.AutoMapperProfiles
{
    public class AdvertProfile : Profile
    {
        public AdvertProfile()
        {
            CreateMap<DataAccessLayer.Models.Advert, AdvertDto>()
                .ForMember(dest => dest.Photo, option => option.MapFrom(source => source.Photos))
                .ReverseMap();

            CreateMap<NewAdvertDto, DataAccessLayer.Models.Advert>()
                .ForMember(dest => dest.CreatedDateTime, option => option.MapFrom(source => System.DateTime.Now))
                .ForMember(dest => dest.Photos, option => option.MapFrom(source => source.Photo));

            CreateMap<RemoveAdvertDto, DataAccessLayer.Models.Advert>()
                .ForMember(dest => dest.Id, option => option.MapFrom(source => source.AdvertId));

            CreateMap<UpdateAdvertDto, DataAccessLayer.Models.Advert>();
        }
    }
}
