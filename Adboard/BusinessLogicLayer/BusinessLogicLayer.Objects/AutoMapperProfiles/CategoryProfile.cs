using Adboard.Contracts.DTOs.Category;
using AutoMapper;

using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Objects.AutoMapperProfiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile() {
            CreateMap<CategoryDto, DataAccessLayer.Models.Category>().ReverseMap();
            CreateMap<NewCategoryDto, DataAccessLayer.Models.Category>().ReverseMap();
        }
    }
}
