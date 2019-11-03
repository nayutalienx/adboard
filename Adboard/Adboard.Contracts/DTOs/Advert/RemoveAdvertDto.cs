using System;
using System.Collections.Generic;
using System.Text;

namespace Adboard.Contracts.DTOs.Advert
{
    public class RemoveAdvertDto
    {
        public string UserId { get; set; }
        public long AdvertId { get; set; }
    }
}
