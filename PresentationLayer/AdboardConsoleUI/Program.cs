using BusinessLogicLayer.Abstraction;
using BusinessLogicLayer.Implementation;
using BusinessLogicLayer.Objects.Advert;
using BusinessLogicLayer.Objects.Comment;
using BusinessLogicLayer.Objects.User;
using DataAccessLayer.StubImplementation;
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
                p($"[id:{ad.AdvertId}] [date:{ad.TimeCreated.ToString("dd MMMM")}] [author:{ad.Author.Name} (phone:{ad.Author.PhoneNumber})] Header:{ad.Header} Description: {ad.Description} Category: {ad.Category}/{ad.SubCategory} Price: {ad.Price}");
        }
        static void p(CommentDto[] comments)
        {
            foreach (var comment in comments)
                p($"{comment.AuthorName}: {comment.Text} [date:{comment.TimeCreated.ToString("dd MMMM")}]");
        }
        static string r()
        {
            return Console.ReadLine();
        }

        static void Main(string[] args)
        {
            //
            // business-logic test
            //

            var serviceCollection = new ServiceCollection()
                .InstallStubDataAccessLayer()
                .InstallBusinessLogic()
                .BuildServiceProvider();

            bool loop = true;

            var advertManager = serviceCollection.GetService<IAdvertManager>();
            var commentManager = serviceCollection.GetService<ICommentManager>();
            var userManager = serviceCollection.GetService<IUserManager>();

            UserDto currentUser = new UserDto { Id = -1, Name = "Гость", PhoneNumber = "" };

            p($"Здраствуйте, {currentUser.Name}");




            while (loop)
            {
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
                            "ad_create {header} {description} {category} {subcategory} {price} // добавить объявление\n" +
                            "ad_getall  // получить список всех объявлений\n" +
                            "ad_getall_my  // получить список своих объявлений\n" +
                            "ad_update {id} {header} {description} {category} {subcategory} {price} // обновить объявление\n" +
                            "ad_remove {id}  // удалить объявление\n" +
                            "search {query}  // поиск по объявлениям\n" +
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
                                PhoneNumber = words[4]
                            });
                            p("Регистрация прошла успешно, теперь залогиньтесь");
                        }
                        catch (Exception ex)
                        {
                            p(ex.Message);
                        }
                        break;
                    case "signout" when words.Length == 1:
                        currentUser = new UserDto { Id = -1, Name = "Гость", PhoneNumber = "" };
                        p($"Здраствуйте, {currentUser.Name}");
                        break;
                    case "ad_create" when words.Length == 6:
                        try
                        {
                            advertManager.Create(new NewAdvertDto
                            {
                                Header = words[1],
                                Description = words[2],
                                Category = words[3],
                                SubCategory = words[4],
                                Price = UInt32.Parse(words[5]),
                                UserId = currentUser.Id
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
                        p(advertManager.GetAll());
                        break;
                    case "ad_getall_my" when words.Length == 1:
                        p("Все ваши объявления");
                        try
                        {
                            p(advertManager.GetAllByUser(currentUser.Id));
                        }
                        catch (Exception ex)
                        {
                            p(ex.Message);
                        }
                        break;
                    case "ad_update" when words.Length == 7:
                        try
                        {
                            advertManager.Update(new UpdateAdvertDto
                            {
                                UserId = currentUser.Id,
                                AdvertId = Int64.Parse(words[1]),
                                Header = words[2],
                                Description = words[3],
                                Category = words[4],
                                SubCategory = words[5],
                                Price = UInt32.Parse(words[6])
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
                            advertManager.Remove(new RemoveAdvertDto
                            {
                                AdvertId = Int64.Parse(words[1]),
                                UserId = currentUser.Id
                            });
                            commentManager.RemoveCommentsByAdvert(Int64.Parse(words[1]));
                            p("Объявление успешно удалено");
                        }
                        catch (Exception ex)
                        {
                            p(ex.Message);
                        }
                        break;
                    case "search" when words.Length == 2:
                        p(advertManager.Search(words[1]));
                        break;
                    case "comment_add" when words.Length == 3:
                        try
                        {
                            commentManager.AddComment(new NewCommentDto
                            {
                                UserId = currentUser.Id,
                                AdvertId = Int64.Parse(words[1]),
                                AuthorName = currentUser.Name,
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
                            p(commentManager.GetCommentsByAdvert(Int64.Parse(words[1])));
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
            //
            // business-logic test end
            //

            

            
        }
    }
}