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
        public static int j = 0;
        public int adv = 0;
        static List<Author> datas = new List<Author>();
        List<Advert> adverts = new List<Advert>();
        public int act;
        public int signout;
        public int Signout
        {
            get
            {
                return signout;
            }
            set
            {
                if (value == 1)
                {
                    Console.WriteLine();
                    adverts.Add(new Advert());
                    adv++;
                    Console.WriteLine("СОЗДАНИЕ ОБЪЯВЛЕНИЯ");
                    Console.WriteLine();
                    Console.Write("Заголовок: ");
                    adverts[adv].Name = Console.ReadLine();
                    Console.Write("Описание: ");
                    adverts[adv].Description = Console.ReadLine();
                    Random rnd = new Random();
                    for (int y = 1; y < adverts.Count; y++)
                    {
                        long id = rnd.Next(100000001, 1000000000);
                        if (adverts[y].Id == id)
                        {
                            long idd = rnd.Next(100000001, 1000000000);
                            adverts[adv].Id = idd;
                        }
                        else
                        {
                            adverts[adv].Id = id;
                        }
                    }
                    adverts[adv].Created = DateTime.Now;
                    adverts[adv].author = datas[j];
                }
                if (value == 2)
                {
                    if (adverts.Count == 0)
                    {
                        Console.WriteLine("Список ваших объявлений пуст");
                    }
                    else
                    {
                        for (int p = 1; p < adverts.Count; p++)
                        {
                            Console.WriteLine(p + "). " + adverts[p].Name);
                        }
                    }
                }
                if (value == 3)
                {
                    for (int f = 1; f < datas.Count; f++)
                    {
                        datas[f].IsAuthorized = false;
                    }
                }
            }
        }
        public int Act
        {
            get
            {
                return act;
            }
            set
            {
                if (value == 1)
                {
                    Console.WriteLine();
                    datas.Add(new Author());
                    j++;
                    Console.WriteLine("ФОРМА РЕГИСТРАЦИИ");
                    Console.WriteLine();
                    Console.Write("Моб. телефон: ");
                    datas[j].phonenumber = Console.ReadLine();
                    Console.Write("Логин: ");
                    datas[j].login = Console.ReadLine();
                    Console.Write("Пароль: ");
                    datas[j].password = Console.ReadLine();
                    Console.WriteLine();
                    Console.WriteLine("Регистрация прошла успешно");
                    datas[j].IsAuthorized = true;
                    Random rnd = new Random();
                    for (int x = 1; x < datas.Count; x++)
                    {
                        long id = rnd.Next(10000000, 100000000);
                        if (datas[x].AuthorId == id)
                        {
                            long idd = rnd.Next(10000000, 100000000);
                            datas[j].AuthorId = idd;
                        }
                        else
                        {
                            datas[j].AuthorId = id;
                        }
                    }
                }
                if (value == 2)
                {
                    string log;
                    string pas;
                    Console.WriteLine();
                    Console.WriteLine("ФОРМА АВТОРИЗАЦИИ");
                    Console.WriteLine();
                    Console.Write("Ваш логин: ");
                    log = Console.ReadLine();
                    Console.Write("Ваш пароль: ");
                    pas = Console.ReadLine();
                    bool dostup = false;
                    int i;
                    for (i = 0; i < datas.Count; i++)
                    {
                        if ((log == datas[i].login) & (pas == datas[i].password))
                        {
                            dostup = true;
                            break;
                        }
                    }
                    if (dostup == true)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Вход выполнен успешно");
                        datas[i].IsAuthorized = true;
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Пароль или логин были введены неверно");
                    }
                }
            }
        }
        public static void Print()
        {
            Console.WriteLine();
            Console.WriteLine("1 - Регистрация");
            Console.WriteLine("2 - Авторизация");
            Console.Write("Ввод: ");
        }

        // test methods

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
                p($"{comment.AuthorName}: {comment.Text}");
        }
        static string r()
        {
            return Console.ReadLine();
        }

        // end test methods


        public static void PrintAuth()
        {
            Console.WriteLine();
            Console.WriteLine("Вы - " + datas[j].login + " (" + datas[j].AuthorId + ")");
            Console.WriteLine("Ваш номер телефона: " + datas[j].phonenumber);
            Console.WriteLine("1 - Создать объявление");
            Console.WriteLine("2 - Просмотреть список моих объявлений");
            Console.WriteLine("3 - Выход из системы");
            Console.Write("Ввод: ");
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
                            "ad_update {id} {header} {description}  // обновить объявление\n" +
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
                    case "ad_update" when words.Length == 4:
                        try
                        {
                            advertManager.Update(new UpdateAdvertDto
                            {
                                UserId = currentUser.Id,
                                AdvertId = Int64.Parse(words[1]),
                                Header = words[2],
                                Description = words[3]
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



            Console.WriteLine("Добро пожаловать!");
            Program vybor = new Program();
            datas.Add(new Author());
            datas[0].IsAuthorized = false;
            while (true)
            {
                try
                {
                    for (int c = j; c < datas.Count; c++)
                    {
                        if (datas[c].IsAuthorized == true)
                        {
                            PrintAuth();
                            vybor.Signout = Convert.ToInt32(Console.ReadLine());
                            break;
                        }
                        if (datas[c].IsAuthorized == false)
                        {
                            Print();
                            vybor.Act = Convert.ToInt32(Console.ReadLine());
                            break;
                        }
                    }
                }
                catch (Exception)
                {
                    break;
                }
            }
        }
    }
}