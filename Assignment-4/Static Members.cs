using System;
using System.IO;

public static class Logger
{
    public static string path { get; set; }
    public static string CurrentUser { get; set; } = "System";

    static Logger()
    {
        string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
        path = Path.Combine(appDirectory, "Default.log");
        Console.WriteLine($"Logger initialized. Default log file: {path}");
        LogMessage("Application initialized");
    }

    public static void LogMessage(string message) 
    {
        string directory = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{CurrentUser}] - {message}\n";
        File.AppendAllText(path, logEntry);
        Console.WriteLine($"Logged: {message}");
    }

    public static void ChangeUser()
    {
        Console.Write("Enter new username: ");
        string newUser = Console.ReadLine();
        LogMessage($"User changed from {CurrentUser} to {newUser}");
        CurrentUser = newUser;
        Console.WriteLine($"Current user is now: {CurrentUser}");
    }
}

class Static_Members
{
    static void Main()
    {
        // Initial setup
        Console.Write("Enter your username: ");
        Logger.CurrentUser = Console.ReadLine();
        Logger.LogMessage($"User '{Logger.CurrentUser}' logged in");

        Console.Write("Enter log file path (press Enter for default path): ");
        string customPath = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(customPath))
        {
            Logger.path = customPath;
            Console.WriteLine($"Using custom log file: {Logger.path}");
            Logger.LogMessage($"Log file path changed to: {Logger.path}");
        }

        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Current User: {Logger.CurrentUser}");
            Console.WriteLine($"Log File: {Logger.path}");
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Add log entry");
            Console.WriteLine("2. View current log file");
            Console.WriteLine("3. Change log file path");
            Console.WriteLine("4. Change user");
            Console.WriteLine("5. Exit");
            Console.Write("Select an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    Console.Write("Enter your log message: ");
                    string message = Console.ReadLine();
                    Logger.LogMessage($"User log entry: {message}");
                    Console.WriteLine("\nPress any key to return to menu...");
                    Console.ReadKey();
                    break;

                case "2":
                    Console.Clear();
                    Logger.LogMessage("User viewed log file");
                    DisplayLogFile();
                    Console.WriteLine("\nPress any key to return to menu...");
                    Console.ReadKey();
                    break;
     
                case "3":
                    Console.Clear();
                    Console.Write("Enter new log file path: ");
                    string newPath = Console.ReadLine();
                    Logger.LogMessage($"Log path changed from {Logger.path} to {newPath}");
                    Logger.path = newPath;
                    Console.WriteLine("\nPress any key to return to menu...");
                    Console.ReadKey();
                    break;

                case "4":
                    Console.Clear();
                    Logger.ChangeUser();
                    Console.WriteLine("\nPress any key to return to menu...");
                    Console.ReadKey();
                    break;

                case "5":
                    Logger.LogMessage("Application closed by user");
                    Console.WriteLine("Goodbye!");
                    return;

                default:
                    Console.Clear();
                    Logger.LogMessage($"Invalid menu option selected: {choice}");
                    Console.WriteLine("Invalid option. Please try again.");
                    Console.WriteLine("\nPress any key to return to menu...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void DisplayLogFile()
    {
        if (File.Exists(Logger.path))
        {
            Console.WriteLine($"Current log contents of {Logger.path}:");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine(File.ReadAllText(Logger.path));
            Console.WriteLine("----------------------------------------");
        }
        else
        {
            Console.WriteLine("Log file doesn't exist yet. Add some log entries first.");
        }
    }
}

