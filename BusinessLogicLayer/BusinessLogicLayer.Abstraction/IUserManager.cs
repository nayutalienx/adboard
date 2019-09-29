using BusinessLogicLayer.Objects.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Abstraction
{
    public interface IUserManager
    {
        void Register(RegisterUserDto dto);
        UserDto Login(LoginUserDto dto);
    }
}
