using ConsoleApp17.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp17.DataAccessLayer.Models
{
    public class User:BaseEntity
    {


        /// <summary>
        /// имя
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// роль пользователя
        /// </summary>
        public Role UserRole { get; set; }
        /// <summary>
        /// логин
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// хэш пароля
        /// </summary>
        public string PasswordHash { get; set; }
        /// <summary>
        /// электронная почта
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// номер телефона
        /// </summary>
        public string TelefNumber { get; set; }
        public ICollection<Advert> Advert { get; set; }
        public ICollection<Comment> Comment { get; set; }
       
        public User()
        {
            Advert = new List<Advert>();
            Comment = new List<Comment>();
           

        }


    }
}
