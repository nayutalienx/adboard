using BusinessLogicLayer.Abstraction;
using BusinessLogicLayer.Implementation;
using BusinessLogicLayer.Objects.Advert;
using BusinessLogicLayer.Objects.Category;
using BusinessLogicLayer.Objects.Comment;
using BusinessLogicLayer.Objects.User;

using Infrastructure.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace at
{
    class Program
    {
        static void p(string s)
        {
            Console.WriteLine(s);
        }
        static void p(AdvertDto[] ads)
        {
            foreach (var ad in ads)
            {
                if (ad != null)
                    p($"[id:{ad.Id}] [date:{ad.CreatedDateTime.ToString("dd MMMM")}] [author:{ad.Author.Name} (phone:{ad.Author.PhoneNumber})] Header:{ad.Header} Description: {ad.Description} Category: {ad.Category.ParentCategoryId}/{ad.Category.Name} Price: {ad.Price} Location:{ad.Location.Country}/{ad.Location.Area}/{ad.Location.City}/{ad.Location.Street}/{ad.Location.HouseNumber}");
            }
        }
        static void p(CategoryDto[] cats)
        {
            foreach (var c in cats)
            {
                if (c != null)
                    p($"{c.Name}/ [id:{c.Id}]");
                if (c.ParentCategoryId != null)
                    p($"{c.ParentCategoryId}");
                if (c.ParentCategory != null && c.ParentCategory.Name != null)
                    p($"{c.ParentCategory.Name}");
                
            }
        }
        static void p(CommentDto[] comments)
        {
            foreach (var comment in comments)
                p($"{comment.Author.Name}: {comment.Text} [date:{comment.CreatedDateTime.ToString("dd MMMM")}]");
        }
        static string r()
        {
            return Console.ReadLine();
        }

        static void Main(string[] args)
        {
            

            var serviceCollection = new ServiceCollection().Install().BuildServiceProvider();

            bool loop = true;

            

            UserDto currentUser = new UserDto { Id = -1, Name = "Гость", PhoneNumber = "" };

            p($"Здраствуйте, {currentUser.Name}");


            AdvertDto[] advertList = null;
            while (loop)
            {
                var advertManager = serviceCollection.GetService<IAdvertManager>();
                var categoryManager = serviceCollection.GetService<ICategoryManager>();
                var userManager = serviceCollection.GetService<IUserManager>();
                p("Введите команду (help - вывод команд)");
                var command = r();
                var words = command.Split(" ");
                switch (words[0])
                {
                    case "help":
                        p("\nUser manager:\n" +
                            "login {email} {password}  // вход\n" +
                            "register {email} {password} {name} {phone}  // регистрация\n" +
                            "signout  // стать гостем\n" +
                            "\nAdvert manager:\n" +
                            "category_add {major} {minor}\n" +
                            "category_getall\n" +
                            "ad_create {header} {description} {category_id} {price} // добавить объявление\n" +
                            "ad_getall  // получить список всех объявлений\n" +
                            "ad_getall_my  // получить список своих объявлений\n" +
                            "ad_update {id} {header} {description} {category_id} {price} // обновить объявление\n" +
                            "ad_remove {id}  // удалить объявление\n" +
                            "доделать  // поиск по фильтру\n" +
                            "\nComment manager:\n" +
                            "comment_add {ad_id} {text}  // добавить комментарий к объявлению\n" +
                            "comment_getall {ad_id}  // посмотреть комментарии объявления\n" +
                            "\nexit //  выход\n"

                            );
                        break;
                    case "login" when words.Length == 3:
                        try
                        {
                            currentUser = userManager.Login(new LoginUserDto
                            {
                                Email = words[1],
                                Password = words[2]
                            });
                            p($"Здраствуйте, {currentUser.Name}");
                        }
                        catch (Exception ex)
                        {
                            p(ex.Message);
                        }
                        break;
                    case "register" when words.Length == 5:
                        try
                        {
                            userManager.Register(new RegisterUserDto
                            {
                                Email = words[1],
                                Password = words[2],
                                Name = words[3],
                                PhoneNumber = words[4],
                                Role = "User"
                            });
                            p("Регистрация прошла успешно, теперь залогиньтесь");
                        }
                        catch (Exception ex) {
                            p(ex.Message);
                        }
                        break;
                    case "signout" when words.Length == 1:
                        currentUser = new UserDto { Id = -1, Name = "Гость", PhoneNumber = "" };
                        p($"Здраствуйте, {currentUser.Name}");
                        break;
                    case "category_add" when words.Length == 3:
                        try
                        {
                            int? temp = Int64.Parse(words[2]) == 0 ? (int?)null : Int32.Parse(words[2]);
                            categoryManager.AddCategory(new NewCategoryDto { Name = words[1], ParentCategoryId = temp });
                            p("Категория успешно добавлена.");
                        }
                        catch (Exception ex) {
                            p(ex.Message);
                        }
                        break;
                    case "category_getall" when words.Length == 1:
                        try {
                            p(categoryManager.GetAllCategories());
                        } catch (Exception ex) {
                            p(ex.Message);
                        }
                        break;
                    case "ad_create" when words.Length == 5:
                        try
                        {
                            advertManager.Create(new NewAdvertDto
                            {
                                Header = words[1],
                                Description = words[2],
                                CategoryId = Int64.Parse(words[3]),
                                Price = UInt32.Parse(words[4]),
                                AuthorId = currentUser.Id,
                                Location = new BusinessLogicLayer.Objects.Address.AddressDto { Country = "Country", Area = "Area", City = "City", HouseNumber = "HouseNumber", Street = "Street"}
                            });
                            p("Объявление успешно добавлено");
                        }
                        catch (Exception ex)
                        {
                            p(ex.Message);
                        }
                        break;
                    case "ad_getall" when words.Length == 1:
                        p("Все объявления:");
                        advertList = advertManager.GetAll();
                        p(advertList);
                        break;
                    case "ad_getall_my" when words.Length == 1:
                        p("Все ваши объявления");
                        try
                        {
                            p(advertManager.GetAdvertsByFilter(new AdvertFilter { UserId = currentUser.Id, Size = 20 }).Items.ToArray());
                        }
                        catch (Exception ex)
                        {
                            p(ex.Message);
                        }
                        break;
                    case "ad_update" when words.Length == 6:
                        try
                        {
                            advertManager.Update(new UpdateAdvertDto
                            {
                                AuthorId = currentUser.Id,
                                Id = Int64.Parse(words[1]),
                                Header = words[2],
                                Description = words[3],
                                CategoryId = Int64.Parse(words[4]),
                                Price = UInt32.Parse(words[5]),
                                Location = new BusinessLogicLayer.Objects.Address.AddressDto { Country = "Country1", Area = "Area1", City = "City1", HouseNumber = "HouseNumber1", Street = "Stree1at" }

                            });
                            p("Объявление успешно обновлено");
                        }
                        catch (Exception ex) 
                        {
                            p(ex.Message);
                        }
                        break;
                    case "ad_remove" when words.Length == 2:
                        try
                        {

                            var ad = new RemoveAdvertDto
                            {
                                AdvertId = Int64.Parse(words[1]),
                                UserId = currentUser.Id
                            };
                            advertManager.Remove(ad);
                            
                            p("Объявление успешно удалено");
                        }
                        catch (Exception ex)
                        {
                            
                            p(ex.Message);
                        }
                        break;
                    case "comment_add" when words.Length == 3:
                        try
                        {
                            advertManager.AddComment(new NewCommentDto
                            {
                                AuthorId = currentUser.Id,
                                AdvertId = Int64.Parse(words[1]),
                                Text = words[2]
                            });
                            p("Комментарий успешно добавлен");
                        }
                        catch (Exception ex)
                        {
                            p(ex.Message);
                        }
                        break;
                    case "comment_getall" when words.Length == 2:
                        p("Список комментариев этого объявления:");
                        try
                        {
                            var ad = advertManager.GetAdvertsByFilter(new AdvertFilter { AdvertId = Int64.Parse(words[1]) }).Items.ToArray()[0];
                            p(ad.Comments);
                        }
                        catch (Exception ex) { p(ex.Message); }
                        break;
                    case "exit":
                        loop = false;
                        p("Нажмите enter чтобы закрыть программу");
                        break;
                    default:
                        p("Неизвестная команда");
                        break;

                }

            }

            Console.ReadKey();
            return;
            
        }
    }
}