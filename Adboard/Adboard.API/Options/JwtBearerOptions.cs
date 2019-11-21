using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adboard.API.Options
{
    public class JwtBearerOptions
    {
        public string Authority { get; set; }
        public string Audience { get; set; }
    }
}
