using Adboard.Contracts;
using Adboard.Contracts.DTOs.Category;
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
    public interface ICategoryApiClient 
    {
        Task<ApiResponse<CategoryDto>> AddCategoryAsync(NewCategoryDto category);
        Task<ApiResponse<IReadOnlyCollection<CategoryDto>>> GetCategoriesAsync();
    }
    public class CategoryApiClient : ApiClient, ICategoryApiClient
    {
        private readonly CategoryApiClientOptions _categoryOptions;
        public CategoryApiClient(
            HttpClient client,
            IHttpContextAccessor accessor,
            IOptions<CategoryApiClientOptions> categoryOptions
            ) : base(client, accessor)
        {
            _categoryOptions = categoryOptions.Value;
        }

        public Task<ApiResponse<CategoryDto>> AddCategoryAsync(NewCategoryDto category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));
            return PostAsync<NewCategoryDto, ApiResponse<CategoryDto>>(_categoryOptions.AddCategoryUrl, category);
        }

        public Task<ApiResponse<IReadOnlyCollection<CategoryDto>>> GetCategoriesAsync()
        {
            return GetAsync<ApiResponse<IReadOnlyCollection<CategoryDto>>>(_categoryOptions.GetCategoriesUrl);
        }
    }
}
