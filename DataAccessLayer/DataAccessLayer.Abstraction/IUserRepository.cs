using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Abstraction
{
    public interface IUserRepository : IRepository<User>
    {
        User Login(string email, string password);
        User Get(string email);
    }
}
