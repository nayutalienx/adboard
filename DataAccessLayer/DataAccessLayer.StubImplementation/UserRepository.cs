using DataAccessLayer.Abstraction;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.StubImplementation
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public User Get(string email)
        {
            throw new NotImplementedException();
        }

        public User Login(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
