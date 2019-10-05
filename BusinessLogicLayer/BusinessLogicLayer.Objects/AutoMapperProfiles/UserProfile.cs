using AutoMapper;
using BusinessLogicLayer.Objects.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Objects.AutoMapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile() {
            CreateMap<DataAccessLayer.Models.User, UserDto>().ReverseMap();
            CreateMap<RegisterUserDto, DataAccessLayer.Models.User>();
            CreateMap<LoginUserDto, DataAccessLayer.Models.User>();

        }
    }
}
