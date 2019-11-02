using BusinessLogicLayer.Objects.Category;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Abstraction
{
    public interface ICategoryManager
    {
        void AddCategory(NewCategoryDto dto);
        CategoryDto[] GetAllCategories();
    }
}
