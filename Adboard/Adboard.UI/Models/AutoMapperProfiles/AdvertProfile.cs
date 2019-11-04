using Adboard.Contracts.DTOs.Address;
using Adboard.Contracts.DTOs.Advert;
using Adboard.Contracts.DTOs.Photo;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adboard.UI.Models.AutoMapperProfiles
{
    public class AdvertProfile : Profile
    {
        public AdvertProfile()
        {
            CreateMap<NewAdvertViewModel, NewAdvertDto>()
                .ForMember(dest => dest.Location, option => option.MapFrom(source => new AddressDto 
                { 
                    Country = source.Country,
                    Area = source.Area,
                    City = source.City,
                    Street = source.Street,
                    HouseNumber = source.HouseNumber

                }))
                .ForMember(dest => dest.Photo, option => option.Ignore())
                .ReverseMap();
        }
    }
}
