using BusinessLogicLayer.Objects.Category;
using BusinessLogicLayer.Objects.Paging;

using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Objects.Advert
{
    public class AdvertFilter : PagingFilter
    {
        public string Header { get; set; }
        public string Description { get; set; }
        public long? CategoryId { get; set; }
        public bool? HasPhotoOnly { get; set; }
        public Range<uint> Price { get; set; }
        public string UserId { get; set; }
        public long? AdvertId { get; set; }

        public Range<DateTime> CreatedDateTime { get; set; }
        
    }
}
