using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public long? ParentCategoryId { get; set; }
        public virtual Category ParentCategory { get; set; }
        public virtual ICollection<Advert> Adverts { get; set; } 
        public virtual ICollection<Category> SubCategories { get; set; } 
    }
}
