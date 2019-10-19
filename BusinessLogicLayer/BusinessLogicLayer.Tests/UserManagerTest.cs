using AutoMapper;
using BusinessLogicLayer.Implementation;
using BusinessLogicLayer.Objects.AutoMapperProfiles;
using DataAccessLayer.Abstraction;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.Tests
{
    public class UserManagerTest
    {
        private readonly Mock<IUserRepository> _userRepository;
        private readonly UserManager _userManager;

        public UserManagerTest() {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(config => {
                config.AddProfile(new UserProfile());
                config.AddProfile(new AdvertProfile());
                config.AddProfile(new CommentProfile());
                config.AddProfile(new CategoryProfile());
                config.AddProfile(new PhotoProfile());
                config.AddProfile(new AddressProfile());
            });

            IMapper mapper = mapperConfiguration.CreateMapper();
            _userRepository = new Mock<IUserRepository>();
            _userManager = new UserManager(_userRepository.Object, mapper);
        }

        [SetUp]
        public void Setup() {

            string email = "mail";
            string password = "password";

            var user = FakeData.GetFakeUsers()[0];

            _userRepository.Setup(ur => ur.Login(email, password)).Returns(user);

        }
        [Test]
        public void TestLogin() {
            // arrange
            string email = "mail";
            string password = "password";
            string name = "Ivan";
            // act
            var userdto = _userManager.Login(new Objects.User.LoginUserDto { Email = email, Password = password });
            // assert
            Assert.AreEqual(name, userdto.Name);
        }
    }
}
