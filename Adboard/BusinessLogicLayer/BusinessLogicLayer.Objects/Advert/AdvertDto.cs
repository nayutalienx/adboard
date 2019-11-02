using BusinessLogicLayer.Objects.Address;
using BusinessLogicLayer.Objects.Category;
using BusinessLogicLayer.Objects.Comment;
using BusinessLogicLayer.Objects.Photo;

using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Objects.Advert
{
    public class AdvertDto
    {
        public long Id { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public CategoryDto Category { get; set; }
        public PhotoDto[] Photo { get; set; }
        public uint Price { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string UserId { get; set; }
        public CommentDto[] Comments { get; set; }
        public AddressDto Location { get; set; }
    }
}
