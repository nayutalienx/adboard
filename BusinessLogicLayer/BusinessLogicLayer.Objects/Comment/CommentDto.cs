using BusinessLogicLayer.Objects.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Objects.Comment
{
    public class CommentDto
    {
        public long AdvertId { get; set; }
        public UserDto Author { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDateTime { get; set; }

    }
}
