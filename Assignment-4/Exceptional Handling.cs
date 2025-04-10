using System;
using System.Collections.Generic;
using System.IO;

class Product
{
    public string Name;
    public int Stock;
    public int Price;

    public Product(string name, int stock, int price)
    {
        Name = name;
        Stock = stock;
        Price = price;
    }
}

class OutOfStockException : Exception
{
    public OutOfStockException(string message) : base(message) { }//passes msg to parent class
}

class Program
{
    static Dictionary<string, Product> inventory = new Dictionary<string, Product>();
    static string inventoryFile = "inventory.txt";
    static string userFile = "users.txt";
    static string logsFile = "purchase_logs.txt";
    static string currentUser = "";

    static void Main()
    {
        InitializeInventory();

        Console.WriteLine("Welcome to Skincare World!");

        while (true)
        {
            Console.WriteLine("\n1. Register\n2. Login as User\n3. Login as Admin\n4. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            if (choice == "1") Register();
            else if (choice == "2") UserLogin();
            else if (choice == "3") AdminPanel();
            else if (choice == "4") break;
            else Console.WriteLine("Invalid choice.");
        }
    }
    
    static void InitializeInventory()
    {
        inventory.Clear(); //clears the exisiting dictinory named inventory
        if (File.Exists(inventoryFile))
        {
            string[] lines = File.ReadAllLines(inventoryFile);
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                if (parts.Length == 4)
                {
                    string keyword = parts[0];
                    string name = parts[1];
                    int stock = int.Parse(parts[2]);
                    int price = int.Parse(parts[3]);
                    inventory[keyword] = new Product(name, stock, price);
                }
            }
        }
        else
        {
            inventory["rose"] = new Product("Rosewater Mist", 50, 250);
            inventory["lav"] = new Product("Lavender Night Cream", 30, 400);
            inventory["cucu"] = new Product("Cucumber Face Pack", 40, 300);
            inventory["aloe"] = new Product("Aloe Vera Gel", 35, 350);
            SaveInventory();
        }
    }

    static void SaveInventory()
    {
        List<string> lines = new List<string>();
        foreach (var item in inventory)
        {
            string line = item.Key + "," + item.Value.Name + "," + item.Value.Stock + "," + item.Value.Price;
            lines.Add(line);
        }
        File.WriteAllLines(inventoryFile, lines);
    }

    static void Register()
    {
        Console.Write("Enter username: ");
        string user = Console.ReadLine();
        Console.Write("Enter password: ");
        string pass = Console.ReadLine();

        if (File.Exists(userFile))
        {
            string[] users = File.ReadAllLines(userFile);
            foreach (string u in users)
            {
                string[] parts = u.Split(',');
                if (parts[0] == user)
                {
                    Console.WriteLine("User already exists!");
                    return;
                }
            }
        }

        File.AppendAllText(userFile, user + "," + pass + "\n");
        Console.WriteLine("Registration successful!");
    }

    static void UserLogin()
    {
        Console.Write("Username: ");
        string user = Console.ReadLine();
        Console.Write("Password: ");
        string pass = Console.ReadLine();

        if (File.Exists(userFile))
        {
            string[] users = File.ReadAllLines(userFile);
            foreach (string u in users)
            {
                string[] parts = u.Split(',');
                if (parts[0] == user && parts[1] == pass)
                {
                    currentUser = user;
                    Console.WriteLine("Login successful!");
                    UserPanel(); //takes user to their personal dashboard
                    return;
                }
            }
        }

        Console.WriteLine("Invalid credentials.");
    }

    static void UserPanel()
    {
        List<string> cart = new List<string>();
        int total = 0;

        while (true)
        {
            Console.WriteLine("\nAvailable Products:");
            foreach (var item in inventory)
            {
                Console.WriteLine($"{item.Key} - {item.Value.Name} (Stock: {item.Value.Stock}, Rs.{item.Value.Price})");
            }

            Console.Write("Enter keyword to buy or 'done' to checkout: ");
            string input = Console.ReadLine().ToLower();
            if (input == "done") break;

            if (inventory.ContainsKey(input))
            {
                Console.Write("Enter quantity (1-10): ");
                try
                {
                    int qty = int.Parse(Console.ReadLine());
                    if (qty < 1 || qty > 10)
                        throw new Exception("Quantity must be between 1 and 10");

                    try
                    {
                        if (inventory[input].Stock < qty)
                            throw new OutOfStockException("Not enough stock.");

                        inventory[input].Stock -= qty;
                        int price = inventory[input].Price * qty;

                        // Apply discount logic: Buy 3 get 1 free
                        if (qty >= 3)
                        {
                            Console.WriteLine("Congrats! You got 1 free.");
                        }

                        cart.Add($"{inventory[input].Name} x{qty} = Rs.{price}");
                        total += price;
                        SaveInventory(); //update the stock to file
                    }
                    catch (OutOfStockException ex) 
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid quantity.");
                }
            }
            else
            {
                Console.WriteLine("Invalid keyword.");
            }
        }

        Console.WriteLine("\nYour Cart:");
        foreach (string item in cart)
            Console.WriteLine(item);

        Console.WriteLine($"Total: Rs.{total}");

        string receipt = $"User: {currentUser}\nDate: {DateTime.Now}\n";
        foreach (string item in cart) receipt += item + "\n"; //Appends each cart item to receipt string
        receipt += $"Total: Rs.{total}\n\n";

        File.AppendAllText(logsFile, receipt);
        File.WriteAllText(currentUser + "_receipt.txt", receipt); 
        Console.WriteLine("Receipt saved.");
    }

    static void AdminPanel()
    {
        Console.Write("Admin Password: ");
        string pass = Console.ReadLine();
        if (pass != "sanjana123")
        {
            Console.WriteLine("Incorrect password.");
            return;
        }

        while (true)
        {
            Console.WriteLine("\nAdmin Panel:");
            Console.WriteLine("1. View Inventory");
            Console.WriteLine("2. Add Product");
            Console.WriteLine("3. Restock Product");
            Console.WriteLine("4. Delete Product");
            Console.WriteLine("5. View Purchase Logs");
            Console.WriteLine("6. Back");
            Console.Write("Choose: ");
            string choice = Console.ReadLine();

            if (choice == "1") ViewInventory();
            else if (choice == "2") AddProduct();
            else if (choice == "3") RestockProduct();
            else if (choice == "4") DeleteProduct();
            else if (choice == "5") ViewLogs();
            else if (choice == "6") break;
            else Console.WriteLine("Invalid option.");
        }
    }

    static void ViewInventory()
    {
        Console.WriteLine("\nCurrent Inventory:");
        foreach (var item in inventory)
        {
            Console.WriteLine($"{item.Key} - {item.Value.Name}, Stock: {item.Value.Stock}, Price: Rs.{item.Value.Price}");
        }
    }

    static void AddProduct()
    {
        Console.Write("Enter keyword: ");
        string keyword = Console.ReadLine().ToLower();

        if (inventory.ContainsKey(keyword))
        {
            Console.WriteLine("Product with this keyword already exists.");
            return;
        }

        Console.Write("Enter product name: ");
        string name = Console.ReadLine();
        Console.Write("Enter stock: ");
        int stock = int.Parse(Console.ReadLine());
        Console.Write("Enter price: ");
        int price = int.Parse(Console.ReadLine());

        inventory[keyword] = new Product(name, stock, price);
        SaveInventory(); 
        Console.WriteLine("Product added.");
    }

    static void RestockProduct()
    {
        Console.Write("Enter keyword to restock: ");
        string keyword = Console.ReadLine().ToLower();

        if (inventory.ContainsKey(keyword))
        {
            Console.Write("Enter quantity to add: ");
            int qty = int.Parse(Console.ReadLine());
            inventory[keyword].Stock += qty;
            SaveInventory();
            Console.WriteLine("Product restocked.");
        }
        else
        {
            Console.WriteLine("Product not found.");
        }
    }

    static void DeleteProduct()
    {
        Console.Write("Enter keyword of product to delete: ");
        string keyword = Console.ReadLine().ToLower();

        if (inventory.ContainsKey(keyword))
        {
            inventory.Remove(keyword);
            SaveInventory();
            Console.WriteLine("Product deleted.");
        }
        else
        {
            Console.WriteLine("Product not found.");
        }
    }

    static void ViewLogs()
    {
        if (File.Exists(logsFile))
        {
            Console.WriteLine("\nPurchase Logs:");
            Console.WriteLine(File.ReadAllText(logsFile));
        }
        else
        {
            Console.WriteLine("No logs found.");
        }
    }
}
