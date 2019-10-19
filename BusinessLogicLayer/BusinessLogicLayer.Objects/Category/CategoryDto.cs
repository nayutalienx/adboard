﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Objects.Category
{
    public class CategoryDto
    {
        public long Id { get; set; }
        public long? ParentCategoryId { get; set; }
        public CategoryDto ParentCategory { get; set; }
        public string Name { get; set; }
        
    }
}
