using BusinessLogicLayer.Objects.Address;
using BusinessLogicLayer.Objects.Category;
using BusinessLogicLayer.Objects.Photo;
using BusinessLogicLayer.Objects.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Objects.Advert
{
    public class UpdateAdvertDto
    {
        public long AuthorId { get; set; }
        public long Id { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public long CategoryId { get; set; }
        public PhotoDto[] Photo { get; set; }
        public uint Price { get; set; }
        public AddressDto Location { get; set; }
    }
}
