using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adboard.UI.Options
{
    public sealed class AdvertApiClientOptions
    {
        public string UpdateAdvertUrl { get; set; }

        public string AddAdvertUrl { get; set; }
        public string DeleteAdvertUrl { get; set; }
        public string AddCommentUrl { get; set; }
        public string GetAdvertsByFilterUrl { get; set; }
    }
}
