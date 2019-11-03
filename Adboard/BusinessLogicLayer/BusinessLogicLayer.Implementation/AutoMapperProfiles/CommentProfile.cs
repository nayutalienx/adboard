using Adboard.Contracts.DTOs.Comment;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Implementation.AutoMapperProfiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<DataAccessLayer.Models.Comment, CommentDto>()
                .ReverseMap();

            CreateMap<NewCommentDto, DataAccessLayer.Models.Comment>()
                .ForMember(dest => dest.CreatedDateTime, option => option.MapFrom(source => System.DateTime.Now));
        }
    }
}
