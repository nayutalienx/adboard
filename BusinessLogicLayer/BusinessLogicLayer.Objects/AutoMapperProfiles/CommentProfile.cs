using AutoMapper;
using BusinessLogicLayer.Objects.Comment;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Objects.AutoMapperProfiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile() {
            CreateMap<DataAccessLayer.Models.Comment, CommentDto>()
                .ForMember(dest => dest.AuthorName, option => option.MapFrom(source => source.Author.Name))
                .ReverseMap();

            CreateMap<NewCommentDto, DataAccessLayer.Models.Comment>();
        }
    }
}
