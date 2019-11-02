using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Objects.Paging
{
    public class Range<T>
    {
        public T From { get; set; }
        public T To { get; set; }
    }
}
