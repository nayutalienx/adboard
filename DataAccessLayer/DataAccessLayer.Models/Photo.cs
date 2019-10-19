using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models
{
    public class Photo : BaseEntity
    {
        public byte[] Data { get; set; }
        public long AdvertId { get; set; }
        public virtual Advert Advert { get; set; }
    }
}
