
using Databas;
using System;
using System.Collections.Generic;
using System.Net;

//Program.cs tar in all användar input från användaren

// Anslutningssträng för att ansluta till SQL Server-databasen
string connectionString = "Server=DESKTOP-8NOJC89\\SQLEXPRESS01;Database=Northwind2023_Mustafa_Ucler;Trusted_Connection=True;";

// Skapar ett objekt av DatabaseManager med anslutningssträngen för att hantera databasoperationer
DatabaseManager dbManager = new DatabaseManager(connectionString);

// Boolsk variabel för att styra huvudloopen i programmet
bool exit = false;

// Visar en meny med olika CRUD-operationer och andra funktioner
while (!exit)
{
    {
        List<string> menuOptions = new List<string>
            {
                "Add Customer",
                "Delete Customer by ID",
                "Update Employee",
                "Show Country Sales",
                "Add Customer and Order",
                "Report generator",
                "Exit"
            };

        // Läser in användarens val från meny
        int choice = ShowMenu(" Database CRUD Operations", menuOptions);

        Console.Clear();

        switch (choice)
        {
            // Hanterar valet att lägga till en ny kund. Be användaren om nödvändig information och anropa sedan AddCustomer-metoden.
            case 0:
                Console.WriteLine("Add Customer , Max 5 letters!");
                string customerId = PromptForInput("Customer ID: ");
                string companyName = PromptForInput("Company Name: ");
                string contactName = PromptForInput("Contact Name: ");
                string contactTitle = PromptForInput("Contact Title: ");

                string address = PromptForInput("Address: ");
                string city = PromptForInput("City: ");
                string region = PromptForInput("Region: ");
                string postalCode = PromptForInput("Postal Code: ");
                string country = PromptForInput("Country: ");
                string phone = PromptForInput("Phone: ");
                string fax = PromptForInput("Fax: ");

                dbManager.AddCustomer(customerId, companyName, contactName, contactTitle, address, city, region, postalCode, country, phone, fax);
                Console.WriteLine("Customer added successfully!");
                break;

            // Hanterar valet att ta bort en kund. Be användaren om kundens ID och anropa sedan DeleteCustomer-metoden.
            case 1:
                Console.WriteLine("Delete Customer");
                customerId = PromptForInput("Enter Customer ID: ");
                dbManager.DeleteCustomerAndOrders(customerId);
                Console.WriteLine("Customer deleted successfully!");
                break;

            // Hanterar valet att uppdaterar en anställd. Be användaren om nödvändig information och anropa sedan UpdateEmployee-metoden.
            case 2:
                Console.WriteLine("Update Employee");
                int employeeId = int.Parse(PromptForInput("Employee ID: "));
                string lastName = PromptForInput("Last Name: ");
                string firstName = PromptForInput("First Name: ");
                string title = PromptForInput("Title: ");
                string titleOfCourtesy = PromptForInput("Title of Courtesy: ");
                DateTime birthDate = DateTime.Parse(PromptForInput("Birth Date (YYYY-MM-DD): "));
                DateTime hireDate = DateTime.Parse(PromptForInput("Hire Date (YYYY-MM-DD): "));
                address = PromptForInput("Address: ");
                city = PromptForInput("City: ");
                region = PromptForInput("Region: ");
                postalCode = PromptForInput("Postal Code: ");
                country = PromptForInput("Country: ");
                string homePhone = PromptForInput("Home Phone: ");
                string extension = PromptForInput("Extension: ");

                dbManager.UpdateEmployee(employeeId, lastName, firstName, title, titleOfCourtesy, birthDate, hireDate, address, city, region, postalCode, country, homePhone, extension);
                Console.WriteLine("Employess updated successfully!");
                break;

            // Visa försäljning per land. Be användaren om land och anropa sedan ShowCountrySales-metoden.
            case 3:
                Console.WriteLine("Show Country Sales");
                country = PromptForInput("Enter Country: ");
                dbManager.ShowCountrySales(country);
                Console.WriteLine("Show country sales successfully!");
                break;

            // Lägg till en ny kund och en ny order för den kunden. Anropa sedan orderItems.Add-metoden.
            case 4:
                Console.WriteLine("Add Customer and Order");
                // Promptar användaren att mata in kund-ID och sparar inmatningen
                customerId = PromptForInput("Customer ID: ");
                companyName = PromptForInput("Company Name: ");
                contactName = PromptForInput("Contact Name: ");
                contactTitle = PromptForInput("Contact Title: ");
                address = PromptForInput("Address: ");
                city = PromptForInput("City: ");
                region = PromptForInput("Region: ");
                postalCode = PromptForInput("Postal Code: ");
                country = PromptForInput("Country: ");
                phone = PromptForInput("Phone: ");
                fax = PromptForInput("Fax: ");

                // Promptar för anställd-ID och konverterar strängen till ett heltal
                employeeId = int.Parse(PromptForInput("Employee ID: "));
                // Promptar för orderdatum och konverterar strängen till ett DateTime-objekt
                DateTime orderDate = DateTime.Parse(PromptForInput("Order Date (YYYY-MM-DD): "));
                // Promptar för önskat leveransdatum
                DateTime requiredDate = DateTime.Parse(PromptForInput("Required Date (YYYY-MM-DD): "));
                // Sätter fraktdatumet till null som standard
                DateTime? shippedDate = null;
                // Promptar för fraktmetod-ID och konverterar strängen till ett heltal
                int shipVia = int.Parse(PromptForInput("Ship Via (Shipping Method ID): "));
                // Promptar för fraktavgiften och konverterar strängen till ett decimaltal
                decimal freight = decimal.Parse(PromptForInput("Freight Amount: "));
                string shipName = PromptForInput("Ship Name: ");
                string shipAddress = PromptForInput("Ship Address: ");
                string shipCity = PromptForInput("Ship City: ");
                string shipRegion = PromptForInput("Ship Region: ");
                string shipPostalCode = PromptForInput("Ship Postal Code: ");
                string shipCountry = PromptForInput("Ship Country: ");

                // Skapar en lista för att lagra orderartiklar
                var orderItems = new List<OrderItem>();
                bool addingItems = true;
                // Loopar för att låta användaren lägga till flera orderartiklar
                while (addingItems)
                {
                    // Loopar för att låta användaren lägga till flera orderartiklar
                    // Promptar för produkt-ID, enhetspris, antal och rabatt för varje artikel
                    int productId = int.Parse(PromptForInput("Enter Product ID: "));
                    decimal unitPrice = decimal.Parse(PromptForInput("Enter Unit Price: "));
                    int quantity = int.Parse(PromptForInput("Enter Quantity: "));
                    float discount = float.Parse(PromptForInput("Enter Discount (0 for none): "));
                    // Lägger till varje ny orderartikel i listan
                    orderItems.Add(new OrderItem { ProductId = productId, UnitPrice = unitPrice, Quantity = quantity, Discount = discount });

                    // Frågar användaren om de vill lägga till fler artiklar
                    string continueAdding = PromptForInput("Add more items? (yes/no): ").ToLower();
                    addingItems = continueAdding == "yes";
                }

                // Anropar metoden för att lägga till kunden och ordern i databasen med alla samlade uppgifter
                dbManager.AddCustomerAndOrder(customerId, companyName, contactName, contactTitle, address, city, region, postalCode, country, phone, fax, employeeId, orderDate, requiredDate, shippedDate, shipVia, freight, shipName, shipAddress, shipCity, shipRegion, shipPostalCode, shipCountry, orderItems);
                Console.WriteLine("Add customer and order successfully!");
                break;

            // Generera en rapport baserad på användarens specifikationer. Anropa sedan GenerateReport-metoden.
            case 5:
                dbManager.GenerateReport();
                break;

            // Avsluta programmet.
            case 6:

                exit = true;
                break;

            default:

                // Hanterar ogiltiga val.
                Console.WriteLine("Invalid option, please try again.");
                break;
        }
        // Menu för bättre design för användaren.

        static int ShowMenu(string prompt, List<string> options)
        {
            Console.WriteLine(prompt);

            Console.CursorVisible = false;

            int width = options.Max(option => option.Length);

            int selected = 0;
            int top = Console.CursorTop;
            for (int i = 0; i < options.Count; i++)
            {
                if (i == selected)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.White;
                }

                var option = options[i];
                Console.WriteLine("- " + option.PadRight(width));

                Console.ResetColor();
            }

            Console.CursorLeft = 0;
            Console.CursorTop = top - 1;

            ConsoleKey? key = null;
            while (key != ConsoleKey.Enter)
            {
                key = Console.ReadKey(intercept: true).Key;

                Console.CursorTop = top + selected;
                string oldOption = options[selected];
                Console.Write("- " + oldOption.PadRight(width));
                Console.CursorLeft = 0;
                Console.ResetColor();

                if (key == ConsoleKey.DownArrow)
                {
                    selected = Math.Min(selected + 1, options.Count - 1);
                }
                else if (key == ConsoleKey.UpArrow)
                {
                    selected = Math.Max(selected - 1, 0);
                }

                Console.CursorTop = top + selected;
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.White;
                string newOption = options[selected];
                Console.Write("- " + newOption.PadRight(width));
                Console.CursorLeft = 0;
                Console.CursorTop = top + selected - 1;
                Console.ResetColor();
            }

            Console.CursorTop = top + options.Count;
            Console.CursorVisible = true;

            return selected;
        }
    }

    // Metod för att läsa in data från användaren.
    string PromptForInput(string message)
    {
        Console.Write(message);
        return Console.ReadLine();
    }
}