
using DataBase.Methods;
using System;
using System.Threading.Tasks;

namespace DataBase
{
    class Program
    {
        const string connectionString = "Server = PC\\SQLEXPRESS; Database = PCAssembly; Trusted_connection = True; TrustServerCertificate=True;";

        static async Task Main()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- Лист проектов ---");
                Console.WriteLine("1. ADO.NET");
                Console.WriteLine("2. Entity Framework");
                Console.WriteLine("0. Выход из программы");
                Console.WriteLine("Выберите режим");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    await AdoNetChoice();
                }
                else if (choice == "2")
                {
                    EFChoice();
                }
                else if (choice == "0")
                {
                    break;
                }
            }
        }

        static async Task AdoNetChoice()
        {
            var crud = new AdoCrud(connectionString);
            while (true)
            {
                Console.Clear();
                Console.WriteLine("*** ADO.NET ***");
                Console.WriteLine("1. Показать проекты");
                Console.WriteLine("2. Добавить проекты");
                Console.WriteLine("3. Завершить проекты");
                Console.WriteLine("4. Удалить проекты");
                Console.WriteLine("0. Назад");
                Console.WriteLine("> ");
                string choiceAdo = Console.ReadLine();

                switch (choiceAdo)
                {
                    case "1":
                        await crud.ShowPcBuildsAsync();
                        break;
                    case "2":
                        Console.Write("Название сборки: ");
                        string name = Console.ReadLine();
                        Console.Write("Общая цена (decimal): ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal price))
                        {
                            await crud.AddPcBuildAsync(name, price);
                        }
                        else
                        {
                            Console.WriteLine("Некорректный формат цены!");
                        }
                        break;
                    case "3":
                        Console.Write("ID сборки: ");
                        if (int.TryParse(Console.ReadLine(), out int id))
                        {
                            await crud.CompletePcBuildAsync(id);
                        }
                        else
                        {
                            Console.WriteLine("Некорректный ID!");
                        }
                        break;
                    case "4":
                        Console.Write("ID сборки для удаления: ");
                        if (int.TryParse(Console.ReadLine(), out int did))
                        {
                            await crud.DeletePcBuildAsync(did);
                        }
                        else
                        {
                            Console.WriteLine("Некорректный ID!");
                        }
                        break;
                    case "0":
                        return;
                }
                Console.WriteLine("Нажмите любую клавишу...");
                Console.ReadKey();
            }
        }

        public static void EFChoice()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("*** Entity Framework - Сборки ПК ***");
                Console.WriteLine("1. Показать сборки");
                Console.WriteLine("2. Добавить сборку");
                Console.WriteLine("3. Завершить сборку");
                Console.WriteLine("4. Удалить сборку");
                Console.WriteLine("0. Назад");
                Console.WriteLine("> ");

                string choiceEf = Console.ReadLine();

                switch (choiceEf)
                {
                    case "1":
                        EFCrud.ShowPcBuilds(); break;
                    case "2":
                        Console.Write("Название сборки: ");
                        string name = Console.ReadLine();

                        Console.Write("Общая цена (decimal): ");
                        if (decimal.TryParse(Console.ReadLine(), out decimal price))
                        {
                            EFCrud.AddPcBuild(name, price);
                        }
                        else
                        {
                            Console.WriteLine("Некорректный формат цены!");
                        }
                        break;
                    case "3":
                        Console.Write("ID сборки: ");
                        if (int.TryParse(Console.ReadLine(), out int id))
                        {
                            EFCrud.CompletePcBuild(id);
                        }
                        else
                        {
                            Console.WriteLine("Некорректный ID!");
                        }
                        break;
                    case "4":
                        Console.Write("ID сборки для удаления: ");
                        if (int.TryParse(Console.ReadLine(), out int did))
                        {
                            EFCrud.DeletePcBuild(did);
                        }
                        else
                        {
                            Console.WriteLine("Некорректный ID!");
                        }
                        break;
                    case "0":
                        return;
                }
                Console.WriteLine("Нажмите любую клавишу...");
                Console.ReadKey();
            
            }
        }

    }
}
