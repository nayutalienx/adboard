using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Tests
{
    public static class FakeData
    {
        public static List<string> GetFakeUsers() {
            return new List<string> {
                "id1",
                "id2"
            };
        }
        public static List<Category> GetFakeCategories() {
            return new List<Category> { 
                new Category
                {
                    Id = 1,
                    Name = "Animals",
                    ParentCategoryId = null
                },
                new Category
                {
                    Id = 2,
                    Name = "Cats",
                    ParentCategoryId = 1
                }
            };
        }
        public static List<Advert> GetFakeAdverts() {
            Category category = new Category
            {
                Id = 1,
                Name = "Animals",
                ParentCategoryId = null
            };

            var users = GetFakeUsers();

            string author = users[0];
            string author1 = users[1];


            return new List<Advert>
            {
                new Advert{
                    Id = 1,
                    UserId = author,
                    Header = "Felix",
                    Description = "Cool cat",
                    CategoryId = category.Id,
                    Category = category,
                    Price = 1500,
                    Location = new Address {
                        Id = 1,
                        Country = "Russia",
                        Area = "Krasnodarsky kray",
                        City = "Krasnodar",
                        Street = "Lenina",
                        HouseNumber = "-43-543",
                        AdvertId = 1,
                    },
                    CreatedDateTime = DateTime.Now,
                    Comments = new List<Comment> {
                        new Comment {
                            Id = 1,
                            Text = "commenttext1",
                            UserId = author,
                            AdvertId = 1,
                            CreatedDateTime = DateTime.Now
                        },
                        new Comment {
                            Id = 2,
                            Text = "commenttext2",
                            UserId = author,
                            AdvertId = 1,
                            CreatedDateTime = DateTime.Now
                        },
                        new Comment {
                            Id = 3,
                            Text = "commenttext3",
                            UserId = author,
                            AdvertId = 1,
                            CreatedDateTime = DateTime.Now
                        }

                    }
                },

                new Advert{
                    Id = 2,
                    UserId = author1,
                    Header = "Felix2",
                    Description = "Cool cat2",
                    CategoryId = category.Id,
                    Category = category,
                    Price = 1600,
                    Location = new Address {
                        Id = 2,
                        Country = "Russia",
                        Area = "Krasnodarsky kray",
                        City = "Krasnodar",
                        Street = "Lenina",
                        HouseNumber = "-43-543",
                        AdvertId = 2,
                    },
                    CreatedDateTime = DateTime.Now,
                    Comments = new List<Comment> {
                        new Comment {
                            Id = 4,
                            Text = "commenttext1",
                            UserId = author,
                            AdvertId = 2,
                            CreatedDateTime = DateTime.Now
                        },
                        new Comment {
                            Id = 5,
                            Text = "commenttext2",
                            UserId = author,
                            AdvertId = 2,
                            CreatedDateTime = DateTime.Now
                        },
                        new Comment {
                            Id = 6,
                            Text = "commenttext3",
                            UserId = author1,
                            AdvertId = 2,
                            CreatedDateTime = DateTime.Now
                        }

                    }
                },
                new Advert{
                    Id = 3,
                    UserId = author1,
                    Header = "Felix3",
                    Description = "Cool cat3",
                    CategoryId = category.Id,
                    Category = category,
                    Price = 1700,
                    Location = new Address {
                        Id = 3,
                        Country = "Russia",
                        Area = "Krasnodarsky kray",
                        City = "Krasnodar",
                        Street = "Lenina",
                        HouseNumber = "-43-543",
                        AdvertId = 3,
                    },
                    CreatedDateTime = DateTime.Now,
                    Comments = new List<Comment> {
                        new Comment {
                            Id = 7,
                            Text = "commenttext1",
                            UserId = author,
                            AdvertId = 3,
                            CreatedDateTime = DateTime.Now
                        },
                        new Comment {
                            Id = 8,
                            Text = "commenttext2",
                            UserId = author,
                            AdvertId = 3,
                            CreatedDateTime = DateTime.Now
                        },
                        new Comment {
                            Id = 9,
                            Text = "commenttext3",
                            UserId = author1,
                            AdvertId = 3,
                            CreatedDateTime = DateTime.Now
                        }

                    }
                }
            };
        }
    }
}
