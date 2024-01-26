using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace Databas
{
    // Klassen DatabaseManager används för att hantera alla CRUD methoder/interaktioner med databasen.
    public class DatabaseManager
    {
        //fält för anslutningssträngen
        string connectionString = "Server=DESKTOP-8NOJC89\\SQLEXPRESS01;Database=Northwind2023_Mustafa_Ucler;Trusted_Connection=True;";

        // Konstruktören tar en anslutningssträng och initierar fältet
        public DatabaseManager(string connectionString)
        {
            this.connectionString = connectionString;
        }

        // Metoden AddCustomer lägger till en ny kund i databasen
        public void AddCustomer(string customerId, string companyName, string contactName, string contactTitle, string address, string city, string region, string postalCode, string country, string phone, string fax)
        {
            // Använder SqlConnection för att skapa en anslutning till databasen med hjälp av anslutningssträngen
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Definierar en SQL-fråga som en sträng för att lägga till en ny kund i tabellen 'Customers'
                string query = @"
            INSERT INTO Customers (
                CustomerID, 
                CompanyName, 
                ContactName, 
                ContactTitle, 
                Address, 
                City, 
                Region, 
                PostalCode, 
                Country, 
                Phone, 
                Fax
            ) VALUES (
                @CustomerID, 
                @CompanyName, 
                @ContactName, 
                @ContactTitle, 
                @Address, 
                @City, 
                @Region, 
                @PostalCode, 
                @Country, 
                @Phone, 
                @Fax
            )";

                // Skapar ett SqlCommand-objekt för att köra SQL-frågan
                SqlCommand command = new SqlCommand(query, connection);
                
                // Lägger till parametrar till SQL-kommandot för att förhindra SQL-injection och förbättra prestanda
                command.Parameters.AddWithValue("@CustomerID", customerId);
                command.Parameters.AddWithValue("@CompanyName", companyName);
                command.Parameters.AddWithValue("@ContactName", contactName);
                command.Parameters.AddWithValue("@ContactTitle", contactTitle);
                command.Parameters.AddWithValue("@Address", address);
                command.Parameters.AddWithValue("@City", city);
                command.Parameters.AddWithValue("@Region", region);
                command.Parameters.AddWithValue("@PostalCode", postalCode);
                command.Parameters.AddWithValue("@Country", country);
                command.Parameters.AddWithValue("@Phone", phone);
                command.Parameters.AddWithValue("@Fax", fax);

                // Öppnar anslutningen till databasen
                connection.Open();
               
                // Utför SQL-kommandot, vilket lägger till den nya kunden i databasen
                command.ExecuteNonQuery();
            }
        }

        public void DeleteCustomerAndOrders(string customerId)
        {
            // Skapar en ny SqlConnection med hjälp av anslutningssträngen
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Öppnar anslutningen till databasen
                connection.Open();

                // Startar en SQL-transaktion för att säkerställa att alla SQL-operationer antingen fullföljs helt eller inte alls
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Justerar customerId så att det har en bestämd längd, har 5 tecken
                    customerId = customerId.PadRight(5);

                    // SQL-fråga för att radera orderdetaljer för en specifik kund
                    string deleteOrderDetailsQuery = "DELETE FROM [Order Details] WHERE OrderID IN (SELECT OrderID FROM Orders WHERE CustomerID = @CustomerID)";
                   
                    // Skapar ett SqlCommand-objekt för att exekvera frågan, och använder den pågående transaktionen
                    SqlCommand deleteOrderDetailsCommand = new SqlCommand(deleteOrderDetailsQuery, connection, transaction);
                    // Sätter parametervärdet för CustomerID i SQL-frågan
                    deleteOrderDetailsCommand.Parameters.AddWithValue("@CustomerID", customerId);
                    // Utför SQL-kommandot
                    deleteOrderDetailsCommand.ExecuteNonQuery();

                    // SQL-fråga för att radera alla order för kunden
                    string deleteOrdersQuery = "DELETE FROM Orders WHERE CustomerID = @CustomerID";
                   
                    // Skapar och förbereder ett nytt SQL-kommando
                    SqlCommand deleteOrdersCommand = new SqlCommand(deleteOrdersQuery, connection, transaction);
                    deleteOrdersCommand.Parameters.AddWithValue("@CustomerID", customerId);
                    deleteOrdersCommand.ExecuteNonQuery();

                    // SQL-fråga för att radera kunden från databasen
                    string deleteCustomerQuery = "DELETE FROM Customers WHERE CustomerID = @CustomerID";
                    SqlCommand deleteCustomerCommand = new SqlCommand(deleteCustomerQuery, connection, transaction);
                    deleteCustomerCommand.Parameters.AddWithValue("@CustomerID", customerId);
                    deleteCustomerCommand.ExecuteNonQuery();

                    // Commitar transaktionen, vilket betyder att alla SQL-operationer sparas i databasen
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // Om ett fel inträffar skrivs felet ut och transaktionen rullas tillbaka
                    Console.WriteLine($"Error: {ex.Message}");
                    transaction.Rollback();
                }
            }
        }

        // Metoden UpdateEmployee uppdaterar information om en anställd
        public void UpdateEmployee(int employeeId, string lastName, string firstName, string title, string titleOfCourtesy, DateTime birthDate, DateTime hireDate, string address, string city, string region, string postalCode, string country, string homePhone, string extension)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Definierar en SQL-fråga som en sträng för att uppdatera en anställd i tabellen 'Employees'
                string query = @"
            UPDATE Employees 
            SET LastName = @LastName, 
                FirstName = @FirstName, 
                Title = @Title, 
                TitleOfCourtesy = @TitleOfCourtesy, 
                BirthDate = @BirthDate, 
                HireDate = @HireDate, 
                Address = @Address, 
                City = @City, 
                Region = @Region, 
                PostalCode = @PostalCode, 
                Country = @Country, 
                HomePhone = @HomePhone, 
                Extension = @Extension 
            WHERE EmployeeID = @EmployeeID";

                // Skapar ett SqlCommand-objekt för att köra SQL-frågan
                SqlCommand command = new SqlCommand(query, connection);

                // Lägger till parametrar till SQL-kommandot. Parametrisering skyddar mot SQL-injection och förbättrar prestanda
                command.Parameters.AddWithValue("@EmployeeID", employeeId);
                command.Parameters.AddWithValue("@LastName", lastName);
                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@Title", title);
                command.Parameters.AddWithValue("@TitleOfCourtesy", titleOfCourtesy);
                command.Parameters.AddWithValue("@BirthDate", birthDate);
                command.Parameters.AddWithValue("@HireDate", hireDate);
                command.Parameters.AddWithValue("@Address", address);
                command.Parameters.AddWithValue("@City", city);
                command.Parameters.AddWithValue("@Region", region);
                command.Parameters.AddWithValue("@PostalCode", postalCode);
                command.Parameters.AddWithValue("@Country", country);
                command.Parameters.AddWithValue("@HomePhone", homePhone);
                command.Parameters.AddWithValue("@Extension", extension);

                // Öppnar anslutningen till databasen
                connection.Open();

                // Utför SQL-kommandot, vilket uppdaterar informationen för den angivna anställda
                command.ExecuteNonQuery();
            }
        }

        // Metoden ShowCountrySales visar försäljning per land
        public void ShowCountrySales(string country)
        {
            // Använder SqlConnection för att skapa en anslutning till databasen med hjälp av anslutningssträngen
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Definierar en SQL-fråga som en sträng för att hämta försäljningssiffror för ett specifikt land
                string query = @"
            SELECT 
                o.EmployeeID, 
                ROUND(SUM(od.UnitPrice * od.Quantity * (1 - od.Discount)), 2) AS TotalSales 
            FROM 
                Orders o
                INNER JOIN [Order Details] od ON o.OrderID = od.OrderID 
            WHERE 
                o.ShipCountry = @Country 
            GROUP BY 
                o.EmployeeID";
                
                // Skapar ett SqlCommand-objekt för att köra SQL-frågan
                SqlCommand command = new SqlCommand(query, connection);
               
                // Lägger till parametern '@Country' till SQL-kommandot för att filtrera resultaten baserat på land
                command.Parameters.AddWithValue("@Country", country);

                // Öppnar anslutningen till databasen
                connection.Open();
                // Använder SqlDataReader för att läsa resultatet av SQL-kommandot
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Loopar igenom alla rader som returneras av SQL-frågan

                    while (reader.Read())
                    {
                        // Skriver ut EmployeeID och TotalSales för varje rad

                        Console.WriteLine($"Employee ID: {reader["EmployeeID"]}, Total Sales: ${reader["TotalSales"]:0.00}");
                    }
                }
            }
        }

        // Metoden AddCustomerAndOrder lägger till en ny kund och en ny order i databasen

        public void AddCustomerAndOrder(string customerId, string companyName, string contactName, string contactTitle, string address, string city, string region, string postalCode, string country, string phone, string fax, int employeeId, DateTime orderDate, DateTime requiredDate, DateTime? shippedDate, int shipVia, decimal freight, string shipName, string shipAddress, string shipCity, string shipRegion, string shipPostalCode, string shipCountry, List<OrderItem> orderItems)
        {
            // Skapar en anslutning till databasen med anslutningssträngen

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Öppnar anslutningen
                connection.Open();
                // Startar en databastransaktion för att säkerställa dataintegritet
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // SQL-fråga för att lägga till en ny kund i databasen
                    string customerQuery = "INSERT INTO Customers (CustomerID, CompanyName, ContactName, ContactTitle, Address, City, Region, PostalCode, Country, Phone, Fax) VALUES (@CustomerID, @CompanyName, @ContactName, @ContactTitle, @Address, @City, @Region, @PostalCode, @Country, @Phone, @Fax)";
                    SqlCommand customerCommand = new SqlCommand(customerQuery, connection, transaction);

                    // Sätter in parametervärden för kunden
                    customerCommand.Parameters.AddWithValue("@CustomerID", customerId);
                    customerCommand.Parameters.AddWithValue("@CompanyName", companyName);
                    customerCommand.Parameters.AddWithValue("@ContactName", contactName);
                    customerCommand.Parameters.AddWithValue("@ContactTitle", contactTitle);
                    customerCommand.Parameters.AddWithValue("@Address", address);
                    customerCommand.Parameters.AddWithValue("@City", city);
                    customerCommand.Parameters.AddWithValue("@Region", region);
                    customerCommand.Parameters.AddWithValue("@PostalCode", postalCode);
                    customerCommand.Parameters.AddWithValue("@Country", country);
                    customerCommand.Parameters.AddWithValue("@Phone", phone);
                    customerCommand.Parameters.AddWithValue("@Fax", fax);
                   
                    customerCommand.ExecuteNonQuery();

                    // SQL-fråga för att lägga till en ny order i databasen
                    string orderQuery = "INSERT INTO Orders (CustomerID, EmployeeID, OrderDate, RequiredDate, ShippedDate, ShipVia, Freight, ShipName, ShipAddress, ShipCity, ShipRegion, ShipPostalCode, ShipCountry) VALUES (@CustomerID, @EmployeeID, @OrderDate, @RequiredDate, @ShippedDate, @ShipVia, @Freight, @ShipName, @ShipAddress, @ShipCity, @ShipRegion, @ShipPostalCode, @ShipCountry); SELECT SCOPE_IDENTITY();";
                    SqlCommand orderCommand = new SqlCommand(orderQuery, connection, transaction);
                    orderCommand.Parameters.AddWithValue("@CustomerID", customerId);
                    orderCommand.Parameters.AddWithValue("@EmployeeID", employeeId);
                    orderCommand.Parameters.AddWithValue("@OrderDate", orderDate);
                    orderCommand.Parameters.AddWithValue("@RequiredDate", requiredDate);
                    orderCommand.Parameters.AddWithValue("@ShippedDate", shippedDate.HasValue ? (object)shippedDate.Value : DBNull.Value);
                    orderCommand.Parameters.AddWithValue("@ShipVia", shipVia);
                    orderCommand.Parameters.AddWithValue("@Freight", freight);
                    orderCommand.Parameters.AddWithValue("@ShipName", shipName);
                    orderCommand.Parameters.AddWithValue("@ShipAddress", shipAddress);
                    orderCommand.Parameters.AddWithValue("@ShipCity", shipCity);
                    orderCommand.Parameters.AddWithValue("@ShipRegion", shipRegion);
                    orderCommand.Parameters.AddWithValue("@ShipPostalCode", shipPostalCode);
                    orderCommand.Parameters.AddWithValue("@ShipCountry", shipCountry);
                   
                    // Utför frågan och sparar det genererade OrderID
                    int orderId = Convert.ToInt32(orderCommand.ExecuteScalar());

                    // Loopar genom varje orderartikel och lägger till dem i databasen
                    foreach (var item in orderItems)
                    {
                        string orderItemQuery = "INSERT INTO [Order Details] (OrderID, ProductID, UnitPrice, Quantity, Discount) VALUES (@OrderID, @ProductID, @UnitPrice, @Quantity, @Discount)";
                        SqlCommand orderItemCommand = new SqlCommand(orderItemQuery, connection, transaction);
                        orderItemCommand.Parameters.AddWithValue("@OrderID", orderId);
                        orderItemCommand.Parameters.AddWithValue("@ProductID", item.ProductId);
                        orderItemCommand.Parameters.AddWithValue("@UnitPrice", item.UnitPrice);
                        orderItemCommand.Parameters.AddWithValue("@Quantity", item.Quantity);
                        orderItemCommand.Parameters.AddWithValue("@Discount", item.Discount);
                        orderItemCommand.ExecuteNonQuery();
                    }

                    // Commitar transaktionen om alla operationer lyckades
                    transaction.Commit();
                }

                // Skriver ut eventuella felmeddelanden och rullar tillbaka transaktionen vid fel
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    transaction.Rollback();
                }
            }
        }

        // Metoden GenerateReport genererar en rapport baserad på användarens specifikationer
        public void GenerateReport()
        {
            // Frågar användaren att ange vilka kolumner som ska ingå i rapporten
            Console.WriteLine("Select columns for the report (comma-separated, e.g., OrderID, CustomerID, OrderDate): ");
            string columns = Console.ReadLine();

            // Frågar användaren att ange eventuella villkor för rapporten
            Console.WriteLine("Enter any conditions (e.g., CustomerID = 'ALFKI', leave blank for none): ");
            string conditions = Console.ReadLine();

            // Frågar användaren att ange sorteringskriterier för rapporten
            Console.WriteLine("Enter sorting column(s) (comma-separated, leave blank for no sorting): ");
            string sortingColumns = Console.ReadLine();

            // Skapar SQL-frågan baserat på användarens input
            string query = ConstructQuery(columns, conditions, sortingColumns);

            // Kör och visar resultatet av SQL-frågan
            ExecuteAndDisplayQuery(query);
        }

        // Metoden ConstructQuery bygger en SQL-fråga baserat på användarens valda kolumner, villkor och sorteringskolumner
        private string ConstructQuery(string columns, string conditions, string sortingColumns)
        {
            // Startar byggandet av SQL-frågan med användarens valda kolumner
            string baseQuery = $"SELECT {columns} FROM Orders ";

            // Lägger till WHERE-villkor om något anges
            if (!string.IsNullOrWhiteSpace(conditions))
            {
                baseQuery += $"WHERE {conditions} ";
            }

            // Lägger till ORDER BY för sorteringskolumnerna om något anges
            if (!string.IsNullOrWhiteSpace(sortingColumns))
            {
                baseQuery += $"ORDER BY {sortingColumns}";
            }

            // Returnerar den färdigbyggda SQL-frågan
            return baseQuery;
        }

        // Metoden ExecuteAndDisplayQuery utför en SQL-fråga och visar resultatet i konsolen
        private void ExecuteAndDisplayQuery(string query)
        {
            // Skapar en anslutning till databasen
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Skapar ett SQL-kommando med den skapade frågan
                SqlCommand command = new SqlCommand(query, connection);

                // Öppnar anslutningen
                connection.Open();

                // Läser data från databasen med SqlDataReader
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Loopar genom resultaten och skriver ut varje rad
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            // Skriver ut varje fält i raden

                            Console.Write($"{reader.GetName(i)}: {reader.GetValue(i)}\t");
                        }
                        Console.WriteLine();
                    }
                }
            }
        }
    }
}
