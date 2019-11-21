using System;
using System.Collections.Generic;
using System.Text;

namespace Adboard.Contracts.DTOs.Paging
{
    public class Range<T>
    {
        public T From { get; set; }
        public T To { get; set; }
    }
}
