using DataAccessLayer.Abstraction;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.StubImplementation
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public bool CheckEmail(string email)
        {
            return Context.Exists(x => { return x.Email.Equals(email); });
        }

        public User Login(string email, string password)
        {
            return Context.Find(x => { return (x.Email.Equals(email) && x.Password.Equals(password)); });
        }
    }
}
