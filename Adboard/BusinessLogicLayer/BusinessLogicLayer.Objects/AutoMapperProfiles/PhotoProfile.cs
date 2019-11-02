using AutoMapper;
using BusinessLogicLayer.Objects.Photo;
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
