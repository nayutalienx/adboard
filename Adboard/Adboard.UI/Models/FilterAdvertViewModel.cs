using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adboard.UI.Models
{
    public class FilterAdvertViewModel
    {
        //
        public string Header { get; set; }
        //
        public string Description { get; set; }
        //
        public long? CategoryId { get; set; }
        //
        public bool HasPhotoOnly { get; set; }
        //
        public uint? PriceFrom { get; set; }
        //
        public uint? PriceTo { get; set; }
        //
        public string UserId { get; set; }
        //
        public long? AdvertId { get; set; }
        //
        public DateTime? CreatedDateTimeFrom { get; set; }
        //
        public DateTime? CreatedDateTimeTo { get; set; }
        public int Size { get; set; }
        public string Region { get; set; }

    }
}
