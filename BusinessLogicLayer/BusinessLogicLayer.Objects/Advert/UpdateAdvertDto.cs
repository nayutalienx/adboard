using BusinessLogicLayer.Objects.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Objects.Advert
{
    public class UpdateAdvertDto
    {
        public long UserId { get; set; }
        public long AdvertId { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public object Photo { get; set; }
        public uint Price { get; set; }
    }
}
