using dependencyBilling.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Customers.Interfaces;
using Customers.Services;
using Customers;
using TableManagement_Repository.Interfaces;
using TableManagement_Repository.Services;
using TableManagementSystem.Factory;
using TableManagementSystem.Interfaces;
using TableManagementSystem.Repository;
using dependencyBilling;
using MenuApp;
using RestaurantManagement;
using Customers.Utility;
using TableManagementSystem.Factory;
using TableManagementSystem.Interfaces;
using TableManagementSystem.Repository;
using RestaurantManagement;  // Adjust namespace according to your project structure

namespace Restaurant
{
    class Program
    {
        public class DummyOrder
        {
            public string Name { get; }
            public List<Guid> Items { get; }

            public DummyOrder(string name, List<Guid> items)
            {
                Name = name;
                Items = items;
            }
        }

        static void CustomerAdminMenu(CustomerService<Customer<ExtendedContactInfo, ExtendedAddress>, ExtendedContactInfo, ExtendedAddress> customerService)
        {
            while (true)
            {
                Console.WriteLine("Admin Menu:");
                Console.WriteLine("1. Add customer");
                Console.WriteLine("2. Update loyalty points");
                Console.WriteLine("3. Add order ID");
                Console.WriteLine("4. Delete customer");
                Console.WriteLine("5. Get Customers");
                Console.WriteLine("6. Get customer by ID");
                Console.WriteLine("7. Back to main menu");

                var adminCommand = Console.ReadLine();

                switch (adminCommand)
                {
                    case "1":
                        AddCustomer.AddCustom(customerService);
                        break;

                    case "2":
                        Console.WriteLine("Enter customer ID to update loyalty points:");
                        if (Guid.TryParse(Console.ReadLine(), out Guid customerId))
                        {
                            Console.WriteLine("Enter loyalty points to add:");
                            var points = int.Parse(Console.ReadLine());
                            customerService.AddLoyaltyPoints(customerId, points);
                            Console.WriteLine("Loyalty points updated successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Invalid customer ID.");
                        }
                        break;

                    case "3":
                        Console.WriteLine("Enter customer ID to add order ID:");
                        if (Guid.TryParse(Console.ReadLine(), out Guid orderIdCustomerId))
                        {
                            Console.WriteLine("Enter order ID to add:");
                            var orderId = int.Parse(Console.ReadLine());
                            customerService.AddOrderId(orderIdCustomerId, orderId);
                            Console.WriteLine("Order ID added successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Invalid customer ID.");
                        }
                        break;

                    case "4":
                        Console.WriteLine("Enter customer ID to delete:");
                        if (Guid.TryParse(Console.ReadLine(), out Guid deleteCustomerId))
                        {
                            customerService.Delete(deleteCustomerId);
                            Console.WriteLine("Customer deleted successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Invalid customer ID.");
                        }
                        break;

                    case "5":
                        Utility.GetAllCustomers(customerService);
                        break;

                    case "6":
                        Utility.GetCustomerById(customerService);
                        break;

                    case "7":
                        return;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void TableAdminMenu(TableService tableService)
        {
            while (true)
            {
                Console.WriteLine("Table Service Operations");
                Console.WriteLine("1. Add Table");
                Console.WriteLine("2. Remove Table");
                Console.WriteLine("3. Reserve Table");
                Console.WriteLine("4. Cancel Table");
                Console.WriteLine("5. List Tables");
                Console.WriteLine("6. Back to main menu");

                int tableInput = Int32.Parse(Console.ReadLine());
                switch (tableInput)
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
        }

        static void CustomerUserMenu(CustomerService<Customer<ExtendedContactInfo, ExtendedAddress>, ExtendedContactInfo, ExtendedAddress> customerService)
        {
            while (true)
            {
                Console.WriteLine("User Menu:");
                Console.WriteLine("1. View customer information");
                Console.WriteLine("2. Add favorite dish");
                Console.WriteLine("3. Add feedback");
                Console.WriteLine("4. Back to main menu");

                var userCommand = Console.ReadLine();

                switch (userCommand)
                {
                    case "1":
                        Console.WriteLine("Enter customer ID to view:");
                        if (Guid.TryParse(Console.ReadLine(), out Guid viewCustomerId))
                        {
                            var customerToView = customerService.GetById(viewCustomerId);
                            Console.WriteLine(customerToView.GetCustomerInfo());
                        }
                        else
                        {
                            Console.WriteLine("Invalid customer ID.");
                        }
                        break;

                    case "2":
                        Console.WriteLine("Enter customer ID to add favorite dish:");
                        if (Guid.TryParse(Console.ReadLine(), out Guid favoriteDishCustomerId))
                        {
                            Console.WriteLine("Enter favorite dish:");
                            var dish = Console.ReadLine();
                            customerService.AddFavoriteDish(favoriteDishCustomerId, dish);
                            Console.WriteLine("Favorite dish added successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Invalid customer ID.");
                        }
                        break;

                    case "3":
                        Console.WriteLine("Enter customer ID to add feedback:");
                        if (Guid.TryParse(Console.ReadLine(), out Guid feedbackCustomerId))
                        {
                            Console.WriteLine("Enter feedback:");
                            var feedback = Console.ReadLine();
                            customerService.AddFeedback(feedbackCustomerId, feedback);
                            Console.WriteLine("Feedback added successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Invalid customer ID.");
                        }
                        break;

                    case "4":
                        return;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void BillingAdminMenu(IBillingService billingService,IOrderManager orderManager)
        {
            while (true)
            {
                Console.WriteLine("Billing Admin Menu:");
                Console.WriteLine("1. List Invoices");
                Console.WriteLine("2. Update Invoice");
                Console.WriteLine("3. Remove Invoice");
                Console.WriteLine("4. Back to main menu");

                var adminBillingCommand = Console.ReadLine();

                switch (adminBillingCommand)
                {
                    case "1":
                        Console.WriteLine("Listing all invoices...");
                        billingService.ListAllInvoices();
                        break;

                    case "2":
                        Console.WriteLine("Enter order Id to update invoice:");
                        int.TryParse(Console.ReadLine(), out int orderId);
                        Console.WriteLine("Enter new amount:");
                        if (decimal.TryParse(Console.ReadLine(), out decimal newAmount))
                        {
                            billingService.PrintInvoice(orderId);
                            Console.WriteLine("Updating invoice...");
                            var oldOrder=orderManager.GetOrder(orderId);
                            billingService.ProcessPaymentAndPrintInvoice(orderId,oldOrder.OrderName, new List<decimal> { newAmount }, "Cash"); // Sample values
                        }
                        else
                        {
                            Console.WriteLine("Invalid amount.");
                        }
                        break;

                    case "3":
                        Console.WriteLine("Enter orderId to remove invoice:");
                        int.TryParse(Console.ReadLine(), out int Id);
                        billingService.DeleteInvoice(Id);
                        Console.WriteLine("Invoice removed successfully!");
                        break;

                    case "4":
                        return;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        


                        break;
                }
            }
        }

        static void BillingUserMenu(IBillingService billingService, IOrderManager orderManager, IMenuRepository menuService)
        {
            while (true)
            {
                Console.WriteLine("Billing User Menu:");
                Console.WriteLine("1. Display Invoice");
                Console.WriteLine("2. Calculate Bill or receive Invoice");
                Console.WriteLine("Back to User Interface");
                var userBillingCommand = Console.ReadLine();

                switch (userBillingCommand)
                {
                    case "1":
                        Console.WriteLine("Enter orderId to display invoice:");
                        int.TryParse(Console.ReadLine(), out int orderId);
                        billingService.PrintInvoice(orderId);
                        break;

                    case "2":
                        Console.WriteLine("Enter your Order Id");
                        if (int.TryParse(Console.ReadLine(), out int Id))
                        {
                            Console.WriteLine("Choose Payment Type (Cash/CreditCard):");
                            var paymentType = Console.ReadLine();
                            var order = orderManager.GetOrder(Id);
                            List<decimal> vals = order.Items.ConvertAll(x => x.Price);
                            billingService.ProcessPaymentAndPrintInvoice(Id,order.OrderName, vals, paymentType);
                            return;
                        }
                        else
                        {
                            Console.WriteLine("Invalid Order ID.");
                        }
                        break;
                    case "3":
                        return;
                        

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void OrderAdminMenu(IOrderManager orderManager, IMenuRepository menuService)
        {
            while (true)
            {
                Console.WriteLine("Order Admin Menu:");
                Console.WriteLine("1. List Orders");
                Console.WriteLine("2. Update Order");
                Console.WriteLine("3. Remove Order");
                Console.WriteLine("4. Back to main menu");

                var adminOrderCommand = Console.ReadLine();

                switch (adminOrderCommand)
                {
                    case "1":
                        Console.WriteLine("Listing all orders...");
                        var orders = orderManager.GetListOfOrders(); // Adjust according to your method
                        foreach (var order in orders)
                        {
                            Console.WriteLine($"Order ID: {order.OrderId}, Customer Name: {order.OrderName}, Items: {string.Join(", ", order.Items)}");
                        }
                        break;

                    case "2":
                        Console.WriteLine("Enter Order ID to update:");
                        if (int.TryParse(Console.ReadLine(), out int orderIdToUpdate))
                        {
                            OrderStatus status;
                            string input = Console.ReadLine();
                            if (Enum.TryParse(input, true, out status) && Enum.IsDefined(typeof(OrderStatus), status))
                            {
                                Console.WriteLine($"You selected: {status}");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Invalid status. Please try again.");
                            }
                            orderManager.UpdateOrderStatus(orderIdToUpdate, status);
                            Console.WriteLine("Order updated successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Invalid Order ID.");
                        }
                        break;

                    case "3":
                        Console.WriteLine("Enter Order ID to remove:");
                        if (int.TryParse(Console.ReadLine(), out int orderIdToRemove))
                        {
                            orderManager.DeleteOrder(orderIdToRemove); // Adjust method signature as needed
                            Console.WriteLine("Order removed successfully!");
                        }
                        else
                        {
                            Console.WriteLine("Invalid Order ID.");
                        }
                        break;

                    case "4":
                        return;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void OrderUserMenu(IOrderManager orderManager,IMenuRepository menuService, string name, int tableNumber,IBillingService billingService)
        {
            while (true)
            {
                // Assuming you have an instance of `OrderManager` available as `orderManager`

                Console.WriteLine("Order User Menu:");
                Console.WriteLine("1. Display Order");
                Console.WriteLine("2. Add Order");
                Console.WriteLine("3. Add Item to Order");
                Console.WriteLine("4. Calculate Bill");
                Console.WriteLine("5. Back to User Menu");

                var userOrderCommand = Console.ReadLine();

                switch (userOrderCommand)
                {
                    case "1":
                        Console.WriteLine("Enter Order ID to display:");
                        if (int.TryParse(Console.ReadLine(), out int orderIdToDisplay))
                        {
                            var order = orderManager.GetOrder(orderIdToDisplay); // Adjust method signature as needed
                            if (order != null)
                            {
                                Console.WriteLine($"Order ID: {order.OrderId}, Customer Name: {order.OrderName}, Table Number: {order.tableNo}, Status: {order.Status}");
                                Console.WriteLine("Items:");
                                foreach (var item in order.GetItems())
                                {
                                    Console.WriteLine($"{item.Name}: {item.Price}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Order not found.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Order ID.");
                        }
                        break;

                    case "2":


                       
                        var newOrder = orderManager.CreateOrder(name, tableNumber);
                        Console.WriteLine($"Order created with ID: {newOrder.OrderId}");
                        
                        
                        break;

                    case "3":
                        Console.WriteLine("Enter Order ID to add item to:");
                        if (int.TryParse(Console.ReadLine(), out int orderIdToAddItem))
                        {
                            MenuItem[] selectedItem = MenuUI.ShowMenu(menuService.GetAllMenuList(), false);
                            
                            foreach (MenuItem item in selectedItem)
                            {
                                orderManager.AddItemToOrder(orderIdToAddItem, item);
                                Console.WriteLine($"Item '{item.Name}' added to Order {orderIdToAddItem}.");
                            }
                            
                            
                        }
                        else
                        {
                            Console.WriteLine("Invalid Order ID.");
                        }
                        break;
                    case "4":
                        Console.WriteLine("Calculate Bill for your order");
                        Console.WriteLine("Enter Order ID for the bill calculation !");
                        if (int.TryParse(Console.ReadLine(), out int orderId))
                        {
                            Console.WriteLine("Choose Payment Type (Cash/CreditCard):");
                            var paymentType = Console.ReadLine();
                            var order = orderManager.GetOrder(orderId);
                            List<decimal> vals = order.Items.ConvertAll(x => x.Price);
                            billingService.ProcessPaymentAndPrintInvoice(orderId,order.OrderName, vals, paymentType);
                            string statusString = "Completed";
                            Enum.TryParse(statusString, out OrderStatus status);
                            orderManager.UpdateOrderStatus(orderId, status);
                            return;
                        }
                        else
                        {
                            Console.WriteLine("Invalid Order ID.");
                        }
                        break;
                        

                    case "5":
                        Console.WriteLine("Returning to main menu...");
                        return;


                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }


            }
        }

        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddSingleton<ICustomerRepository<Customer<ExtendedContactInfo, ExtendedAddress>, ExtendedContactInfo, ExtendedAddress>,
                                  CustomerService<Customer<ExtendedContactInfo, ExtendedAddress>, ExtendedContactInfo, ExtendedAddress>>();
            services.AddSingleton<CustomerService<Customer<ExtendedContactInfo, ExtendedAddress>, ExtendedContactInfo, ExtendedAddress>>();
            services.AddSingleton<ITableRepository, TableRepository>()
                .AddSingleton<IBillingDatabase, BillingDatabase>()
                .AddSingleton<IBillingService, BillingService>()
                .AddSingleton<ITableFactory, TableFactory>()
                .AddSingleton<IMenuRepository, MenuRepository>()
                .AddTransient<TableService>()
                .AddTransient<IOrderManager, OrderManager>(); // Add IOrderManager service

            var serviceProvider = services.BuildServiceProvider();

            var customerService = serviceProvider.GetService<CustomerService<Customer<ExtendedContactInfo, ExtendedAddress>, ExtendedContactInfo, ExtendedAddress>>();
            var menuService = serviceProvider.GetService<IMenuRepository>();
            var tableService = serviceProvider.GetService<TableService>();
            var billingService = serviceProvider.GetService<IBillingService>();
            var orderManager = serviceProvider.GetService<IOrderManager>();

            while (true)
            {
                Console.WriteLine("Select View:");
                Console.WriteLine("1. Admin View");
                Console.WriteLine("2. User View");
                Console.WriteLine("3. Exit");

                int input;
                Int32.TryParse(Console.ReadLine(), out input);
                switch (input)
                {
                    case 1:
                        {
                            Console.WriteLine("Select Admin Service:");
                            Console.WriteLine("1. Table Service");
                            Console.WriteLine("2. Customer Service");
                            Console.WriteLine("3. Menu Service");
                            Console.WriteLine("4. Billing Service");
                            Console.WriteLine("5. Order Service");
                            Console.WriteLine("6. Exit");

                            int adminInput;
                            Int32.TryParse(Console.ReadLine(), out adminInput);

                            switch (adminInput)
                            {
                                case 1:
                                    TableAdminMenu(tableService);
                                    break;
                                case 2:
                                    CustomerAdminMenu(customerService);
                                    break;
                                case 3:
                                    MenuUI.showMenuAdmin(menuService);
                                    break;
                                case 4:
                                    BillingAdminMenu(billingService,orderManager);
                                    break;
                                case 5:
                                    OrderAdminMenu(orderManager, menuService);
                                    break;
                                case 6:
                                    return;
                            }
                        }
                        break;
                    case 2:
                        {
                            // create user 
                            Guid customerId = AddCustomer.AddCustom(customerService);
                            var customer1 = Utility.GetCustomerById(customerService);

                            int tableNumber = tableService.ReserveTable(customer1.Name);
                            if(tableNumber == -1)
                            {
                                return;
                            }
                            
                            // allocate table
                            Console.WriteLine("Select User Service:");
                            Console.WriteLine("1. Billing Service");
                            Console.WriteLine("2. Order Service");
                            Console.WriteLine("3. Exit");

                            int userInput;
                            Int32.TryParse(Console.ReadLine(), out userInput);

                            switch (userInput)
                            {
                                case 1:
                                    BillingUserMenu(billingService,orderManager,menuService);
                                    break;
                                case 2:
                                    OrderUserMenu(orderManager, menuService, customer1.Name, tableNumber, billingService);
                                    break;
                                case 3:
                                    return;
                            }
                        }
                        break;
                    case 3:
                        return;
                }
            }
        }
    }
}
