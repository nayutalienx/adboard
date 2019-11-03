using Adboard.Contracts.DTOs.Address;
using AutoMapper;

using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Objects.AutoMapperProfiles
{
    public class AddressProfile : Profile
    {
        public AddressProfile() {
            CreateMap<AddressDto, DataAccessLayer.Models.Address>().ReverseMap();
        }
    }
}
