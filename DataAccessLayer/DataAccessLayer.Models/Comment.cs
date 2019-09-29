using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models
{
    public class Comment : BaseEntity
    {

        /// <summary>
        /// дата и время создания комментария
        /// </summary>
        public DateTime CreatedDateTime { get; set; }
        /// <summary>
        /// текст комментария
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// автор комментария
        /// </summary>
        public string AuthorName { get; set; }
        public long AdvertId { get; set; }

    }
}
