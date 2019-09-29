using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Objects.Advert
{
    public class NewAdvertDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public long UserId { get; set; }
    }
}
