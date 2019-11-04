using Adboard.Contracts;
using Adboard.Contracts.DTOs.Advert;
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
    public interface IIdentityClient {
        Task<ApiResponse<UserDto>> GetUserInfoAsync(string id);
    }
    public class IdentityClient : ApiClient, IIdentityClient
    {
        private readonly IdentityClientOptions _identityOptions;
        public IdentityClient(
            HttpClient client,
            IHttpContextAccessor accessor,
            IOptions<IdentityClientOptions> identityOptions
            ) : base(client, accessor)
        {
            _identityOptions = identityOptions.Value;

        }

        public Task<ApiResponse<UserDto>> GetUserInfoAsync(string id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));
            return GetAsync<ApiResponse<UserDto>>($"{_identityOptions.UserInfoByIdUrl}/{id}");
        }
    }
}
