using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Objects.Category
{
    public class NewCategoryDto
    {
        public long? ParentCategoryId { get; set; }
        public string Name { get; set; }
    }
}
