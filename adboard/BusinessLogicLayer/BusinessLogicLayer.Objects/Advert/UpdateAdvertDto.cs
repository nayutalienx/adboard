using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Objects.Advert
{
    public class UpdateAdvertDto
    {
        public long UserId { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
