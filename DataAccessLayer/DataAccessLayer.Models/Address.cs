using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models
{
    public class Address : BaseEntity
    {
        public string Country { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public long AdvertId { get; set; }
        public virtual Advert Advert { get; set; }
    }
}
