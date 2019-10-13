using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models
{
    public class Category : BaseEntity
    {
        public string Major { get; set; }
        public string Minor { get; set; }
        public virtual ICollection<Advert> Adverts { get; set; }
    }
}
