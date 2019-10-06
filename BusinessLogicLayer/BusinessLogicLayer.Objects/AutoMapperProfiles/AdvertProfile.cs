using AutoMapper;
using BusinessLogicLayer.Objects.Advert;
using BusinessLogicLayer.Objects.Comment;
using BusinessLogicLayer.Objects.User;

namespace BusinessLogicLayer.Objects.AutoMapperProfiles
{
    public class AdvertProfile : Profile
    {
        public AdvertProfile()
        {
            CreateMap<DataAccessLayer.Models.Advert, AdvertDto>().ReverseMap();

            CreateMap<NewAdvertDto, DataAccessLayer.Models.Advert>()
                .ForMember(dest => dest.CreatedDateTime, option => option.MapFrom(source => System.DateTime.Now));

            CreateMap<RemoveAdvertDto, DataAccessLayer.Models.Advert>()
                .ForMember(dest => dest.Id, option => option.MapFrom(source => source.AdvertId));

            CreateMap<UpdateAdvertDto, DataAccessLayer.Models.Advert>()
                .ForMember(dest => dest.AuthorId, option => option.MapFrom(source => source.UserId))
                .ForMember(dest => dest.Id, option => option.MapFrom(source => source.AdvertId));
        }
    }
}
