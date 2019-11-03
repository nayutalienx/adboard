using System;
using System.Collections.Generic;
using System.Text;

namespace Adboard.Contracts.DTOs.Address
{
    public class AddressDto
    {
        public string Country { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
    }
}
