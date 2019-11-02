using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Objects.Paging
{
    public class PagingResult<T>
    {
        public List<T> Items { get; set; }
        public int TotalRows { get; set; }
    }
}
