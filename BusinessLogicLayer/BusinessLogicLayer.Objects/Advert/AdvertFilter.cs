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
        public Range<DateTime> CreatedDateTime { get; set; }
        public AdvertFilter() {
            CreatedDateTime = new Range<DateTime>();
        }
    }
}
