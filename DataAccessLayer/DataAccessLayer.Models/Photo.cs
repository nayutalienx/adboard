using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models
{
    public class Photo : BaseEntity
    {
        public byte[] Data { get; set; }
    }
}
