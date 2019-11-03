
using Adboard.Contracts.DTOs.Advert;
using Adboard.Contracts.DTOs.Comment;
using Adboard.Contracts.DTOs.Paging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Abstraction
{
    public interface IAdvertManager
    {
        Task<AdvertDto> CreateAsync(NewAdvertDto dto);
        Task<IReadOnlyCollection<AdvertDto>> GetAllAsync();
        Task<AdvertDto> UpdateAsync(UpdateAdvertDto dto);
        Task RemoveAsync(RemoveAdvertDto dto);
        Task<PagingResult<AdvertDto>> GetAdvertsByFilterAsync(AdvertFilter filter);
        Task<CommentDto> AddCommentAsync(NewCommentDto dto);
        Task<AdvertDto> GetAsync(long id);
        Task<IReadOnlyCollection<AdvertDto>> GetAllByUserIdAsync(string userid);
    }

}
