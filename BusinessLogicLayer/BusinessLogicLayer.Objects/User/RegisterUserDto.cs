using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Objects.User
{
    public class RegisterUserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }

    }
}
