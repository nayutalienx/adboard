using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Objects.Comment
{
    public class CommentDto
    {
        public long AdvertId { get; set; }
        public long AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDateTime { get; set; }

    }
}
