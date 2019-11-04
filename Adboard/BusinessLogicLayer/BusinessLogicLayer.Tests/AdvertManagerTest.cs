using Adboard.Contracts.DTOs.Advert;
using AutoMapper;
using BusinessLogicLayer.Implementation;
using BusinessLogicLayer.Implementation.AutoMapperProfiles;
using DataAccessLayer.Abstraction;
using DataAccessLayer.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            // advertsQueryable.FirstOrDefault(ad => ad.Id == advert_id)
            _advertRepository.Setup(ad => ad.GetAsync(advert_id)).Returns(Task.Delay(1).ContinueWith(t => advertsQueryable.FirstOrDefault(ad => ad.Id == advert_id)));
            _advertRepository.Setup(ad => ad.GetAllAsync()).Returns(Task.Delay(1).ContinueWith(t => advertsQueryable));
            _categoryRepository.Setup(cat => cat.GetAllAsync()).Returns(Task.Delay(1).ContinueWith(t => categoriesQueryable));


        }

        [Test]
        public async Task TestAdvertFiltersAsync()
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

            var result = await _advertManager.GetAdvertsByFilterAsync(advertFilter);

            // assert

            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public async Task TestAdvertGetByIdAsync() {
            // arrange
            int testIndex = 1;
            // act
            var advertDto = await _advertManager.GetAsync(testIndex);

            // assert
            Assert.AreEqual("Felix", advertDto.Header);
        }
        [Test]
        public async Task TestGetAdvertByUserAsync() {
            // arrange
            string id = "id2";
            // act
            var advertDtos = await _advertManager.GetAllByUserIdAsync(id);

            // assert
            foreach (var ad in advertDtos)
                Assert.AreEqual(ad.UserId, id);

            Assert.AreEqual(advertDtos.Count, 2);
        }
        [Test]
        public async Task TestGetAllAsync() {
            // arrange
            int length = 3;
            // act
            var ads = await _advertManager.GetAllAsync();
            // assert
            Assert.AreEqual(length, ads.Count);
        }
        [Test]
        public async Task TestGetAllCategoriesAsync() {
            // arrange 
            int length = 2;
            // act 
            var cats = await _categoryManager.GetAllCategoriesAsync();
            // assert
            Assert.AreEqual(length, cats.Count);
        }
    }
}