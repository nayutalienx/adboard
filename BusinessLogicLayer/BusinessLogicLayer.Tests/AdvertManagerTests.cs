using BusinessLogicLayer.Abstraction;
using Infrastructure.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BusinessLogicLayer.Tests
{
    public class AdvertManagerTests
    {
        ServiceProvider serviceCollection;
        IAdvertManager advertManager;

        public AdvertManagerTests() {
            serviceCollection = new ServiceCollection().Install().BuildServiceProvider();
            advertManager = serviceCollection.GetService<IAdvertManager>();
        }

        [Fact]
        public void SearchAdvertTest() {
            // arrange
            

            string query = "desc1";
            string result = "head1";

            // act

            var advert = advertManager.Search(query);

            // assert
            Assert.Equal(result, advert[0].Header);
        }

        [Fact]
        public void GetAdvertByIdTest() {
            // arrange
            long id = 2;
            string header = "head2";

            //act
            var advert = advertManager.Get(id);

            // assert
            Assert.Equal(header, advert.Header);
        }
    }
}
