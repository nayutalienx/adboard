using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Objects.Comment
{
    public class NewCommentDto
    {
        public string UserId { get; set; }
        public long AdvertId { get; set; }
        public string Text { get; set; }

    }
}
