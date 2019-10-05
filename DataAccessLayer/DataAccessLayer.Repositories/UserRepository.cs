using DataAccessLayer.Abstraction;
using DataAccessLayer.EntityFramework;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLayer.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AdboardContext context) : base(context)
        {
        }

        public bool CheckEmail(string email)
        {
            return Entity.Where(u => u.Email.Equals(email)).SingleOrDefault() == null ? false : true;
        }

        public User Login(string email, string password)
        {
            return Entity.FirstOrDefault(u => u.Email.Equals(email) && u.Password.Equals(password));
        }
    }
}
