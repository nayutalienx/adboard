using AutoMapper;
using BusinessLogicLayer.Objects.Category;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Objects.AutoMapperProfiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile() {
            CreateMap<CategoryDto, DataAccessLayer.Models.Category>().ReverseMap();
        }
    }
}
