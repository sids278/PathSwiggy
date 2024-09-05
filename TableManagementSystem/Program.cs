using Microsoft.Extensions.DependencyInjection;
using System;
using TableManagement_Repository.Interfaces;
using TableManagement_Repository.Services;
using TableManagementSystem.Factory;
using TableManagementSystem.Interfaces;
using TableManagementSystem.Repository;

namespace TableManagement_Repository
{
    class Program
    {
        ServiceCollection serviceProvider;
        static void Main(string[] args)
        {
            // Set up Dependency Injection
            var serviceProvider = new ServiceCollection()
                .AddSingleton<ITableRepository, TableRepository>()
                .AddSingleton<ITableFactory, TableFactory>()  // Register the factory
                .AddTransient<TableService>()
                .BuildServiceProvider();

            var tableService = serviceProvider.GetService<TableService>();

            while (true)
            {
                Console.WriteLine("Table Service Operations");
                Console.WriteLine("1. Add Table");
                Console.WriteLine("2. Remove Table");
                Console.WriteLine("3. Reserve Table");
                Console.WriteLine("4. Cancel Table");
                Console.WriteLine("5. List Tables");
                Console.WriteLine("6. Go back");

                Console.WriteLine("Enter the operation number:");
                int input = Int32.Parse(Console.ReadLine());    
                switch (input)
                {
                    case 1:
                        Console.WriteLine("Enter the Table number:");
                        int tableNumber = Int32.Parse(Console.ReadLine());
                        tableService.AddTable(tableNumber);
                        break;
                    case 2:
                        Console.WriteLine("Enter the Table number:");
                        tableNumber = Int32.Parse(Console.ReadLine());
                        tableService.RemoveTable(tableNumber);
                        break;
                    case 3:
                        Console.WriteLine("Enter the user name:");
                        string userName = Console.ReadLine();
                        tableService.ReserveTable(userName);
                        break;
                    case 4:
                        Console.WriteLine("Enter the user name:");
                        userName = Console.ReadLine();
                        tableService.CancelTable(userName);
                        break;
                    case 5:
                        tableService.ListTables();
                        break;
                    case 6:
                        return;
                    default:
                        Console.WriteLine("Enter a valid input");
                        break;
                }
            }
            return;
            tableService.AddTable(1);
            for (int i = 2; i <= 10; i++)
            {
                tableService.AddTable(i);
            }

            tableService.ReserveTable("Alice");
            tableService.ReserveTable("Bob");
            tableService.ReserveTable("Charlie");
            tableService.ReserveTable("Dave");
            tableService.ReserveTable("Eve");

            Console.WriteLine();
            tableService.ListTables();
            Console.WriteLine();

            // Testing table removal
            tableService.RemoveTable(8);
            tableService.CancelTable("Charlie");
            tableService.RemoveTable(8);
            Console.WriteLine();

            tableService.ListTables();
            Console.ReadKey();
        }
    }
}
