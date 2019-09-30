using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{

    public class Advert : BaseEntity
    {  /// <summary>
       /// название объявления
       /// </summary>
        public string Topic { get; set; }
        /// <summary>
        /// описание объявления
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// категория объявления
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// подкатегория
        /// </summary>
        public string Subcategory { get; set; }
        /// <summary>
        /// дата и время создания
        /// </summary>
        public DateTime CreatedDataTime { get; set; }
        /// <summary>
        /// автор
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// фото
        /// </summary>
        public object Photo { get; set; }

        /// <summary>
        /// цена
        /// </summary>
        public uint Price { get; set; }
    }
}