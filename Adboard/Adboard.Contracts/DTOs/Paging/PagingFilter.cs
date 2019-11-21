using System;
using System.Collections.Generic;
using System.Text;

namespace Adboard.Contracts.DTOs.Paging
{
    public class PagingFilter
    {
        public int CurrentPage { get; set; }
        public int Size { get; set; }
    }
}
