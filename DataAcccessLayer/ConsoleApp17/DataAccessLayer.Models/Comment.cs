using ConsoleApp17.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp17.DataAccessLayer.Models
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
        public User Author { get; set; }
    }
}
