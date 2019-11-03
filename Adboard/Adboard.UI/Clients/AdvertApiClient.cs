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
        //Task<ApiResponse<AdvertDto>> UpdateAdvertAsync(UpdateAdvertDto advert);
        Task<ApiResponse<AdvertDto>> AddAdvertAsync(NewAdvertDto advert);
        //Task<ApiResponse> RemoveAdvertAsync(RemoveAdvertDto advert);
        //Task<ApiResponse<CommentDto>> AddCommentAsync(NewCommentDto comment);
        Task<ApiResponse<IReadOnlyCollection<AdvertDto>>> GetAdvertsByFilterAsync(AdvertFilter filter);
    }
    public class AdvertApiClient : ApiClient, IAdvertApiClient
    {
        private readonly AdvertApiClientOptions _advertOptions;
        private readonly CategoryApiClientOptions _cateogoryOptions;
        public AdvertApiClient(
            HttpClient client,
            IHttpContextAccessor accessor,
            IOptions<AdvertApiClientOptions> advertOptions,
            IOptions<CategoryApiClientOptions> categoryOptions) : base(client, accessor)
        {
            _advertOptions = advertOptions.Value;
            _cateogoryOptions = categoryOptions.Value;
        }
        public Task<ApiResponse<AdvertDto>> AddAdvertAsync(NewAdvertDto advert)
        {
            if (advert == null)
                throw new ArgumentNullException(nameof(advert));
            return PostAsync<NewAdvertDto, ApiResponse<AdvertDto>>(_advertOptions.AddAdvertUrl, advert);
        }

        public Task<ApiResponse<IReadOnlyCollection<AdvertDto>>> GetAdvertsByFilterAsync(AdvertFilter filter)
        {
            if (filter == null)
                throw new ArgumentNullException(nameof(filter));
            return PostAsync<AdvertFilter, ApiResponse<IReadOnlyCollection<AdvertDto>>>(_advertOptions.GetAdvertsByFilterUrl, filter);
        }
    }
}
