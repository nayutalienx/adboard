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