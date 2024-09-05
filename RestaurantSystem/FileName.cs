using Customers.Services;

using System;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using System.Threading.Tasks;

namespace Customers.Utility

{

    static public class AddCustomer

    {

        static public Guid AddCustom(CustomerService<Customer<ExtendedContactInfo, ExtendedAddress>, ExtendedContactInfo, ExtendedAddress> customerService)

        {

            Console.WriteLine("Enter customer name:");

            var name = Console.ReadLine();

            Console.WriteLine("Enter contact phone:");

            var phone = Console.ReadLine();

            Console.WriteLine("Enter contact email:");

            var email = Console.ReadLine();

            Console.WriteLine("Enter alternative phone:");

            var altPhone = Console.ReadLine();

            Console.WriteLine("Enter address line 1:");

            var line1 = Console.ReadLine();

            Console.WriteLine("Enter city:");

            var city = Console.ReadLine();

            Console.WriteLine("Enter state:");

            var state = Console.ReadLine();

            Console.WriteLine("Enter zip code:");

            var zipCode = Console.ReadLine();

            Console.WriteLine("Enter country:");

            var country = Console.ReadLine();

            Console.WriteLine("Enter address line 2 (optional):");

            var line2 = Console.ReadLine();

            var customer = new Customer<ExtendedContactInfo, ExtendedAddress>(

                Guid.NewGuid(), name,

                new ExtendedContactInfo(phone, email, altPhone),

                new ExtendedAddress(line1, city, state, zipCode, country, line2));

            Guid customerId = customerService.Add(customer);

            Console.WriteLine($"Customer added successfully! Customer ID: {customerId}");

            return customerId;

        }

    }

}

