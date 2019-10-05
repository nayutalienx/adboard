using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{

    public class Advert : BaseEntity
    {  
        public string Header { get; set; }
        
        public string Description { get; set; }
       
        public string Category { get; set; }
        
        public string SubCategory { get; set; }
        
        public DateTime CreatedDateTime { get; set; }
       
        public long AuthorId { get; set; }
        public virtual User Author { get; set; }
        
        public uint Price { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}