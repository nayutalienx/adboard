using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models
{

    public class Role
    {
        public enum Roles
        {
            Admin = 1,
            User = 2,
            NonUser = 3

        }
        public Roles RoleId;
    }
}
