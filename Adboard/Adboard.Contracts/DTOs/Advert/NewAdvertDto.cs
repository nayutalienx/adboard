using Adboard.Contracts.DTOs.Address;
using Adboard.Contracts.DTOs.Photo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adboard.Contracts.DTOs.Advert
{
    public class NewAdvertDto
    {
        public string Header { get; set; }
        public string Description { get; set; }
        public long CategoryId { get; set; }
        public PhotoDto[] Photo { get; set; }
        public uint Price { get; set; }
        public string UserId { get; set; }
        public AddressDto Location { get; set; }
    }
}
