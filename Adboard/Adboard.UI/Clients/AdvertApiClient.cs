using Adboard.Contracts;
using Adboard.Contracts.DTOs.Advert;
using Adboard.Contracts.DTOs.Comment;
using Adboard.UI.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Adboard.UI.Clients
{
    public interface IAdvertApiClient
    {
        Task<ApiResponse<AdvertDto>> UpdateAdvertAsync(UpdateAdvertDto advert);
        Task<ApiResponse<AdvertDto>> AddAdvertAsync(NewAdvertDto advert);
        Task<ApiResponse> RemoveAdvertAsync(RemoveAdvertDto advert);
        Task<ApiResponse<CommentDto>> AddCommentAsync(NewCommentDto comment);
        Task<ApiResponse<IReadOnlyCollection<AdvertDto>>> GetAdvertsByFilterAsync(AdvertFilter filter);
    }
    public class AdvertApiClient : ApiClient, IAdvertApiClient
    {
        private readonly AdvertApiClientOptions _advertOptions;
        
        public AdvertApiClient(
            HttpClient client,
            IHttpContextAccessor accessor,
            IOptions<AdvertApiClientOptions> advertOptions
            ) : base(client, accessor)
        {
            _advertOptions = advertOptions.Value;
            
        }
        public Task<ApiResponse<AdvertDto>> AddAdvertAsync(NewAdvertDto advert)
        {
            if (advert == null)
                throw new ArgumentNullException(nameof(advert));
            return PostAsync<NewAdvertDto, ApiResponse<AdvertDto>>(_advertOptions.AddAdvertUrl, advert);
        }

        public Task<ApiResponse<CommentDto>> AddCommentAsync(NewCommentDto comment)
        {
            if (comment == null)
                throw new ArgumentNullException(nameof(comment));
            return PostAsync<NewCommentDto, ApiResponse<CommentDto>>(_advertOptions.AddCommentUrl, comment);
        }

        public Task<ApiResponse<IReadOnlyCollection<AdvertDto>>> GetAdvertsByFilterAsync(AdvertFilter filter)
        {
            if (filter == null)
                throw new ArgumentNullException(nameof(filter));
            return PostAsync<AdvertFilter, ApiResponse<IReadOnlyCollection<AdvertDto>>>(_advertOptions.GetAdvertsByFilterUrl, filter);
        }

        public Task<ApiResponse> RemoveAdvertAsync(RemoveAdvertDto advert)
        {
            if (advert == null)
                throw new ArgumentNullException(nameof(advert));
            return DeleteAsync<RemoveAdvertDto, ApiResponse>(_advertOptions.DeleteAdvertUrl, advert);
        }

        public Task<ApiResponse<AdvertDto>> UpdateAdvertAsync(UpdateAdvertDto advert)
        {
            if (advert == null)
                throw new ArgumentNullException(nameof(advert));
            return PutAsync<UpdateAdvertDto, ApiResponse<AdvertDto>>(_advertOptions.UpdateAdvertUrl, advert);
        }
    }
}
