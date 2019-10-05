using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Objects.Comment
{
    public class NewCommentDto
    {
        public long AuthorId { get; set; }
        public long AdvertId { get; set; }
        public string Text { get; set; }

    }
}
