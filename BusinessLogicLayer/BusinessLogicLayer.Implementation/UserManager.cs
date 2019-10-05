using AutoMapper;
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
        private readonly IMapper _mapper;
        public UserManager(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public UserDto Login(LoginUserDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
                throw new ArgumentException();

            var user = _userRepository.Login(dto.Email, dto.Password);

            if (user == null)
                throw new Exception("Данные введены неверено или такого пользователя нет");
            
            return _mapper.Map<UserDto>(user);
            
        }

        public void Register(RegisterUserDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password) || string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.PhoneNumber))
                throw new ArgumentException();

            var user = _userRepository.CheckEmail(dto.Email);

            if (user)
                throw new Exception("Пользователь с таким email уже существует");

            _userRepository.Add(_mapper.Map<User>(dto));

            _userRepository.SaveChanges();
        }
    }
}
