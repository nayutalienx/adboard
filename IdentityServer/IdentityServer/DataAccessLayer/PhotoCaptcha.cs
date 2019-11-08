using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.DataAccessLayer
{
    public class PhotoCaptcha
    {
        public int Id { get; set; }
        public byte[] Data { get; set; }
        public string Answer { get; set; }
    }
}
