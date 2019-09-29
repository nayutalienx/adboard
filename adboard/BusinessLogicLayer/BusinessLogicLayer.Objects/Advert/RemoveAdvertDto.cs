using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Objects.Advert
{
    public class RemoveAdvertDto
    {
        public long UserId { get; set; }
        public long AdvertId { get; set; }
    }
}
