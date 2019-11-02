using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{

    public class Advert : BaseEntity
    {  
        public string Header { get; set; }  
        public string Description { get; set; }
        public long CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string UserId { get; set; }
        public uint Price { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
        public virtual Address Location { get; set; }
    }
}