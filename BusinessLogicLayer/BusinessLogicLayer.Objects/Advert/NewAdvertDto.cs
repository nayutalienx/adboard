﻿using BusinessLogicLayer.Objects.Address;
using BusinessLogicLayer.Objects.Category;
using BusinessLogicLayer.Objects.Photo;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Objects.Advert
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
