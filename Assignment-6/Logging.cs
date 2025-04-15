using System;
using System.Collections.Generic;
using System.IO;
using Serilog;

class Product
{
    public int Id;
    public string Name;
    public decimal Price;
    public int Stock;
}

class User
{
    public string Username;
    public string Password;
}

class ShoppingSystem
{
    private List<Product> products = new List<Product>();
    private List<User> users = new List<User>();
    private User currentUser;
    private List<(Product, int)> cart = new List<(Product, int)>();
    private string userFile = "users.txt";

    public ShoppingSystem()
    {
        InitializeProducts();
        InitializeLogger();
        LoadUsers();
    }

    private void InitializeProducts()
    {
        products.Add(new Product { Id = 1, Name = "Laptop", Price = 999.99m, Stock = 10 });
        products.Add(new Product { Id = 2, Name = "Phone", Price = 699.99m, Stock = 15 });
        products.Add(new Product { Id = 3, Name = "Headphones", Price = 149.99m, Stock = 25 });
        products.Add(new Product { Id = 4, Name = "Monitor", Price = 299.99m, Stock = 8 });
        products.Add(new Product { Id = 5, Name = "Keyboard", Price = 49.99m, Stock = 30 });
    }

    private void InitializeLogger()
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.File("logs/system.log")
            .WriteTo.Console()
            .CreateLogger();

        Log.Information("System started at {Now}", DateTime.Now);
    }

    public void Run()
    {
        while (true)
        {
            Console.Clear();
            if (currentUser == null)
            {
                ShowBox("Shopping System - Main Menu");
                ShowAuthMenu();
            }
            else
            {
                ShowBox($"Welcome {currentUser.Username}");
                ShowMainMenu();
            }
        }
    }

    private void ShowBox(string title)
    {
        Console.WriteLine("╔" + new string('═', 50) + "╗");
        Console.WriteLine($"║{title.PadLeft((50 + title.Length) / 2).PadRight(50)}║");
        Console.WriteLine("╚" + new string('═', 50) + "╝");
    }

    private void ShowAuthMenu()
    {
        Console.WriteLine("1. Login");
        Console.WriteLine("2. Register");
        Console.WriteLine("3. Exit");
        Console.Write("Choose an option: ");
        string choice = Console.ReadLine();
        if (choice == "1") Login();
        else if (choice == "2") Register();
        else if (choice == "3") Environment.Exit(0);
    }

    private void Login()
    {
        Console.Write("Username: ");
        string username = Console.ReadLine();
        Console.Write("Password: ");
        string password = Console.ReadLine();

        User found = null;
        foreach (var u in users)
        {
            if (u.Username == username && u.Password == password)
            {
                found = u;
                break;
            }
        }

        if (found != null)
        {
            currentUser = found;
            Log.Information("{User} logged in", username);
            CreateUserLogger();
        }
        else
        {
            Log.Error("Failed login for {User}", username);
            Console.WriteLine("Invalid username or password.");
            Console.ReadKey();
        }
    }

    private void Register()
    {
        Console.Write("New Username: ");
        string username = Console.ReadLine();
        Console.Write("New Password: ");
        string password = Console.ReadLine();

        User user = new User { Username = username, Password = password };
        users.Add(user);
        SaveUsers();
        Log.Information("User registered: {User}", username);
        Console.WriteLine("Registration successful!");
        Console.ReadKey();
    }

    private void SaveUsers()
    {
        using (StreamWriter writer = new StreamWriter(userFile))
        {
            foreach (var user in users)
            {
                writer.WriteLine($"{user.Username},{user.Password}");
            }
        }
    }

    private void LoadUsers()
    {
        if (!File.Exists(userFile)) return;

        string[] lines = File.ReadAllLines(userFile);
        foreach (string line in lines)
        {
            string[] parts = line.Split(',');
            if (parts.Length == 2)
            {
                users.Add(new User { Username = parts[0], Password = parts[1] });
            }
        }
    }

    private void CreateUserLogger()
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.File($"logs/users/{currentUser.Username}.log")
            .WriteTo.Console()
            .CreateLogger();
    }

    private void ShowMainMenu()
    {
        Console.WriteLine("1. View Products");
        Console.WriteLine("2. Add to Cart");
        Console.WriteLine("3. Checkout");
        Console.WriteLine("4. Logout");
        Console.Write("Choose an option: ");
        string choice = Console.ReadLine();

        if (choice == "1") ShowProducts();
        else if (choice == "2") AddToCart();
        else if (choice == "3") Checkout();
        else if (choice == "4") Logout();
    }

    private void ShowProducts()
    {
        ShowBox("Available Products");
        Console.WriteLine($"{"ID",-5}{"Name",-20}{"Price",-15}{"Stock",-10}");
        Console.WriteLine(new string('-', 55));
        foreach (Product p in products)
        {
            Console.WriteLine($"{p.Id,-5}{p.Name,-20}${p.Price,-15}${p.Stock,-10}");
        }
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    private void AddToCart()
    {
        ShowProducts();
        try
        {
            Console.Write("\nEnter Product ID: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Enter Quantity: ");
            int qty = int.Parse(Console.ReadLine());

            Product found = null;
            foreach (var p in products)
            {
                if (p.Id == id)
                {
                    found = p;
                    break;
                }
            }

            if (found == null || found.Stock < qty)
            {
                Console.WriteLine("Invalid product or not enough stock.");
                return;
            }

            found.Stock -= qty;
            cart.Add((found, qty));
            Log.Information("Added to cart: {Qty} x {Product}", qty, found.Name);
            Console.WriteLine($"Added {qty} x {found.Name} to cart.");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error while adding to cart");
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private void Checkout()
    {
        Console.Clear();
        if (cart.Count == 0)
        {
            Console.WriteLine("Your cart is empty.");
            Console.ReadKey();
            return;
        }

        ShowBox("Cart Summary");
        decimal total = 0;
        Console.WriteLine($"{"Qty",-5}{"Product",-20}{"Unit Price",-15}{"Total",-15}");
        Console.WriteLine(new string('-', 60));
        foreach (var (product, qty) in cart)
        {
            decimal itemTotal = product.Price * qty;
            Console.WriteLine($"{qty,-5}{product.Name,-20}${product.Price,-15}${itemTotal,-15}");
            total += itemTotal;
        }

        Console.WriteLine($"\nTotal Amount: ${total}");
        Console.Write("Confirm purchase (y/n)? ");
        string confirm = Console.ReadLine();
        if (confirm.ToLower() == "y")
        {
            cart.Clear();
            Log.Information("Purchase completed. Total: ${Total}", total);
            Console.WriteLine("Thank you for your purchase!");
        }
        else
        {
            Console.WriteLine("Checkout cancelled.");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private void Logout()
    {
        Log.Information("{User} logged out", currentUser.Username);
        currentUser = null;
        Log.CloseAndFlush();
    }
}

class Program
{
    static void Main()
    {
        ShoppingSystem system = new ShoppingSystem();
        system.Run();
    }
}
