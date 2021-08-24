using ex9_ORM_EFCore_Console.Database;
using ex9_ORM_EFCore_Console.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ex9_ORM_EFCore_Console.Models;
using Microsoft.EntityFrameworkCore;

namespace ex9_ORM_EFCore_Console
{
    public class Startup
    {
        public async Task Start()
        {
            string action = string.Empty;
            while (action != "exit")
            {
                Console.WriteLine("Виберіть дію яку бажаєте виконати:\n" +
                    "1 - Додати деякі заготовлені дані\n" +
                    "2 - Роздрукувати їх в консоль\n" +
                    "3 - Провести деякі зміни для клієнта igor_123 та видалити клієнта kris555\n" +
                    "4 - Очистити базу данних\n" +
                    "exit - Завершити роботу програми\n");

                action = Console.ReadLine();
                try
                {
                    await ChooseAction(action);
                } catch (Exception e)
                {
                    Console.WriteLine("Ой! Сталася помилка...." + e.Message);
                }
                
            }
        }

        private async Task ChooseAction(string action)
        {
            int.TryParse(action, out int result);
            switch ((MenuEnum)result)
            {
                case MenuEnum.ADD_DATA:
                    await InitSomeDataAsync();
                    Console.WriteLine("Дані успішно додані");
                    break;
                case MenuEnum.PRINT:
                    await GetData();
                    break;
                case MenuEnum.UPDATE_AND_REMOVE:
                    await UpdateAndDeleteData();
                    Console.WriteLine("База даних успішно оновлена!");
                    break;
                case MenuEnum.CLEAR_DB:
                    await ClearDB();
                    Console.WriteLine("База даних успішно очищена!");
                    break;
                default:
                    Console.WriteLine("Введення було некоректне!!!");
                    break;
            }
        }

        private Task InitSomeDataAsync()
        {
            return Task.Run(() =>
            {
                using (var db = new ApplicationContext())
                {
                    // clients
                    var igor = new Client { UserName = "igor_123" };
                    var kristina = new Client { UserName = "kris555" };
                    var yana = new Client { UserName = "yanka753" };

                    db.Clients.AddRange(igor, kristina, yana);

                    // their info 
                    var igorInfo = new ClientInfo
                    {
                        FirstName = "Igor",
                        LastName = "Ivanov",
                        Age = 45,
                        Sex = "Male",
                        Address = "SomeIgorAddress",
                        Client = igor
                    };

                    var kristinaInfo = new ClientInfo
                    {
                        FirstName = "Kristina",
                        LastName = "Pazyuk",
                        Age = 22,
                        Sex = "Female",
                        Address = "SomeKristinaAddress",
                        Client = kristina
                    };

                    var yanaInfo = new ClientInfo
                    {
                        FirstName = "Yana",
                        LastName = "Ivasyuk",
                        Age = 20,
                        Sex = "Female",
                        Address = "SomeYanaAddress",
                        Client = yana
                    };

                    db.ClientInfos.AddRange(igorInfo, kristinaInfo, yanaInfo);


                    // cards

                    // for igor
                    var igorCardPrivatBank = new Card
                    {
                        Number = "4952 5459 3704 9942",
                        Client = igor
                    };

                    var igorCardOschadBank = new Card
                    {
                        Number = "4110 2087 4053 7111",
                        Client = igor
                    };

                    // for kristina 
                    var kristinaCardPrivatBank = new Card
                    {
                        Number = "4387 2289 7024 1537",
                        Client = kristina
                    };

                    var kristinaCardMonoBank = new Card
                    {
                        Number = "4278 9576 7829 3190",
                        Client = kristina
                    };

                    var kristinaCardPayPal = new Card
                    {
                        Number = "5559 3223 1949 2858",
                        Client = kristina
                    };

                    // for yana

                    var yanaCardOschadBank = new Card
                    {
                        Number = "5380 2548 1620 6207",
                        Client = yana
                    };

                    db.Cards.AddRange(
                        igorCardPrivatBank,
                        igorCardOschadBank,
                        kristinaCardPrivatBank,
                        kristinaCardMonoBank,
                        kristinaCardPayPal,
                        yanaCardOschadBank);


                    // banks

                    var privatBank = new Bank
                    {
                        Name = "Приват Банк",
                        Clients = new List<Client> { igor, kristina }
                    };

                    var oschadBank = new Bank
                    {
                        Name = "ОщадБанк",
                        Clients = new List<Client> { igor, yana }
                    };

                    var monoBank = new Bank
                    {
                        Name = "MonoBank",
                        Clients = new List<Client> { kristina }
                    };

                    var payPalBank = new Bank
                    {
                        Name = "PayPal",
                        Clients = new List<Client> { kristina }
                    };

                    db.Banks.AddRange(privatBank, oschadBank, monoBank, payPalBank);

                    db.SaveChanges();
                }
            });
        }
        private async Task GetData()
        {
            using (var db = new ApplicationContext())
            {
                var clients = await db.Clients
                        .Include(cl => cl.ClientInfo)
                        .Include(cl => cl.Cards)
                        .Include(cl => cl.Banks)
                        .ToListAsync();

                foreach (var client in clients)
                {
                    Console.WriteLine($"Username = {client.UserName}\n" +
                        $"{client.ClientInfo}");

                    Console.WriteLine("Cards: ");
                    foreach (var card in client.Cards)
                    {
                        Console.WriteLine($"\tNumber = {card.Number}");
                    }

                    Console.WriteLine("\nBanks: ");
                    foreach (var bank in client.Banks)
                    {
                        Console.WriteLine($"\tName of bank = {bank.Name}");
                    }
                    Console.WriteLine("\n");
                }
            }
        }
        private async Task UpdateAndDeleteData()
        {
            using (var db = new ApplicationContext())
            {
                // updating
                var igor = await db.Clients.SingleAsync(cl => cl.UserName == "igor_123");
                db.Entry(igor).Collection(client => client.Cards).Load();
                igor.Cards.Add(new Card { Number = "5303 4521 4141 2110" });

                var payPalBank = await db.Banks.SingleAsync(bank => bank.Name == "PayPal");
                db.Entry(payPalBank).Collection(bank => bank.Clients).Load();
                payPalBank.Clients.Add(igor);

                //removing
                var kristina = await db.Clients.SingleAsync(cl => cl.UserName == "kris555");

                db.Clients.Remove(kristina);

                db.SaveChanges();
            }
        }
        private Task ClearDB()
        {
            return Task.Run(() =>
            {
                using (var db = new ApplicationContext())
                {
                    db.Database.EnsureDeleted();
                    db.Database.EnsureCreated();
                }
            });
        }
    }
}
