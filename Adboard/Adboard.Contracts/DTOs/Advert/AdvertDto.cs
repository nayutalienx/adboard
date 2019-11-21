using Adboard.Contracts.DTOs.Address;
using Adboard.Contracts.DTOs.Category;
using Adboard.Contracts.DTOs.Comment;
using Adboard.Contracts.DTOs.Photo;
using System;
using System.Collections.Generic;
using System.Text;

namespace Adboard.Contracts.DTOs.Advert
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
