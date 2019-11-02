using AutoMapper;
using BusinessLogicLayer.Implementation;
using BusinessLogicLayer.Objects.Advert;
using BusinessLogicLayer.Objects.AutoMapperProfiles;
using DataAccessLayer.Abstraction;
using DataAccessLayer.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogicLayer.Tests
{
    public class AdvertManagerTest
    {
        private readonly Mock<IAdvertRepository> _advertRepository;
        private readonly Mock<ICategoryRepository> _categoryRepository;
        private readonly CategoryManager _categoryManager;
        private readonly AdvertManager _advertManager;

        public AdvertManagerTest() {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(config => {
                config.AddProfile(new AdvertProfile());
                config.AddProfile(new CommentProfile());
                config.AddProfile(new CategoryProfile());
                config.AddProfile(new PhotoProfile());
                config.AddProfile(new AddressProfile());
            });

            IMapper mapper = mapperConfiguration.CreateMapper();

            _advertRepository = new Mock<IAdvertRepository>();
            _categoryRepository = new Mock<ICategoryRepository>();

            _advertManager = new AdvertManager(_advertRepository.Object, mapper);
            _categoryManager = new CategoryManager(_categoryRepository.Object, mapper);

        
        }
        [SetUp]
        public void Setup()
        {
            int advert_id = 1;

            var advertsQueryable = FakeData.GetFakeAdverts().AsQueryable();
            var categoriesQueryable = FakeData.GetFakeCategories().AsQueryable();

            _advertRepository.Setup(ad => ad.Get(advert_id)).Returns(advertsQueryable.FirstOrDefault(ad => ad.Id == advert_id));
            _advertRepository.Setup(ad => ad.GetAll()).Returns(advertsQueryable);
            _categoryRepository.Setup(cat => cat.GetAll()).Returns(categoriesQueryable);


        }

        [Test]
        public void TestAdvertFilters()
        {
            // arrange
            var advertFilter = new AdvertFilter {
                Header = "Felix2",
                Size = 2,
                CurrentPage = 1,
                CreatedDateTime = null,
                Description = null,
                CategoryId = null,
                HasPhotoOnly = null,
                Price = null
            };

            // act

            var result = _advertManager.GetAdvertsByFilter(advertFilter);

            // assert

            Assert.AreEqual(1, result.TotalRows);
        }

        [Test]
        public void TestAdvertGetById() {
            // arrange
            int testIndex = 1;
            // act
            var advertDto = _advertManager.Get(testIndex);

            // assert
            Assert.AreEqual("Felix", advertDto.Header);
        }
        [Test]
        public void TestGetAdvertByUser() {
            // arrange
            string id = "id2";
            // act
            AdvertDto[] advertDtos = _advertManager.GetAllByUserId(id);

            // assert
            foreach (var ad in advertDtos)
                Assert.AreEqual(ad.UserId, id);

            Assert.AreEqual(advertDtos.Length, 2);
        }
        [Test]
        public void TestGetAll() {
            // arrange
            int length = 3;
            // act
            var ads = _advertManager.GetAll();
            // assert
            Assert.AreEqual(length, ads.Length);
        }
        [Test]
        public void TestGetAllCategories() {
            // arrange 
            int length = 2;
            // act 
            var cats = _categoryManager.GetAllCategories();
            // assert
            Assert.AreEqual(length, cats.Length);
        }
    }
}