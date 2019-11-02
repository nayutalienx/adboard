using AutoMapper;
using BusinessLogicLayer.Abstraction;
using BusinessLogicLayer.Objects.Category;
using DataAccessLayer.Abstraction;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public void AddCategory(NewCategoryDto dto)
        {
            _categoryRepository.Add(_mapper.Map<Category>(dto));
            _categoryRepository.SaveChanges();
        }

        public CategoryDto[] GetAllCategories()
        {
            return _mapper.Map<CategoryDto[]>(_categoryRepository.GetAll().ToArray());
        }

    }
}
