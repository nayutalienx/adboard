
using Adboard.Contracts.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Abstraction
{
    public interface ICategoryManager
    {
        Task<CategoryDto> AddCategoryAsync(NewCategoryDto dto);
        Task<IReadOnlyCollection<CategoryDto>> GetAllCategoriesAsync();
    }
}
