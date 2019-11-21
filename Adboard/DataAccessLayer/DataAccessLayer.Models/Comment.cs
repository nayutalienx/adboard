using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models
{
    public class Comment : BaseEntity
    {
        public DateTime CreatedDateTime { get; set; }
        public string Text { get; set; }
        public string UserId { get; set; }
        public long AdvertId { get; set; }
        public virtual Advert Advert { get; set; }
         

    }
}
