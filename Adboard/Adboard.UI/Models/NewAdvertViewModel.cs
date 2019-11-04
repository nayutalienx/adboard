using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adboard.UI.Models
{
    public class NewAdvertViewModel
    {
        public string Header { get; set; }
        public string Description { get; set; }
        public long CategoryId { get; set; }
        public IFormFile Photo { get; set; }
        public uint Price { get; set; }
        public string UserId { get; set; }
        public string Country { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
    }
}
