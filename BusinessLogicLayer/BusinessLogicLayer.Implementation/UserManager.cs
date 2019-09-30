using BusinessLogicLayer.Abstraction;
using BusinessLogicLayer.Objects.User;
using DataAccessLayer.Abstraction;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Implementation
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;
        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public UserDto Login(LoginUserDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
                throw new ArgumentException();

            var user = _userRepository.Login(dto.Email, dto.Password);

            if (user == null)
                throw new Exception("Данные введены неверено или такого пользователя нет");

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber
            };
        }

        public void Register(RegisterUserDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password) || string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.PhoneNumber))
                throw new ArgumentException();

            var user = _userRepository.CheckEmail(dto.Email);

            if (user)
                throw new Exception("Пользователь с таким email уже существует");

            _userRepository.Add(new User
            {
                Name = dto.Name,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                Password = dto.Password
            });

        }
    }
}
