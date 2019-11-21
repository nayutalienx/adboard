using Adboard.Contracts.DTOs.Category;
using AutoMapper;
using BusinessLogicLayer.Abstraction;

using DataAccessLayer.Abstraction;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Implementation
{
    public class CategoryManager : ICategoryManager
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryManager(ICategoryRepository categoryRepository, IMapper mapper) {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryDto> AddCategoryAsync(NewCategoryDto dto)
        {
            var result = await _categoryRepository.AddAsync(_mapper.Map<Category>(dto));
            await _categoryRepository.SaveChangesAsync();
            return _mapper.Map<CategoryDto>(result);
        }

        public async Task<IReadOnlyCollection<CategoryDto>> GetAllCategoriesAsync()
        {
            return _mapper.Map<IReadOnlyCollection<CategoryDto>>(await _categoryRepository.GetAllAsync());
        }

    }
}
