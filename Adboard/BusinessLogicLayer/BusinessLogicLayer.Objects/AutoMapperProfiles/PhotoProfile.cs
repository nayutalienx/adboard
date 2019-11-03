using Adboard.Contracts.DTOs.Photo;
using AutoMapper;

using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Objects.AutoMapperProfiles
{
    public class PhotoProfile : Profile
    {
        public PhotoProfile() {
            CreateMap<PhotoDto, DataAccessLayer.Models.Photo>().ReverseMap();
        }
    }
}
