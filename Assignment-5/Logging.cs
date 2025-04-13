using System;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.File;

class Program
{
    static void Main()
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("logs/Biswanth.log")
            .CreateLogger();

        try
        {
            Log.Information("Application started");
            RunCalculator();
        }
        catch (Exception ex)
        {
            Log.Fatal($"Application terminated unexpectedly: {ex.Message}");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    static void RunCalculator()
    {
        while (true)
        {
            Console.WriteLine("\nCalculator Menu:");
            Console.WriteLine("1. Divide two numbers");
            Console.WriteLine("2. Exit");
            Console.Write("Select option: ");

            var choice = Console.ReadLine();

            if (choice == "2")
            {
                Log.Information("User chose to exit the application");
                break;
            }

            if (choice == "1")
            {
                PerformDivision();
            }
            else
            {
                Log.Warning($"User entered invalid menu option: {choice}");
                Console.WriteLine("Invalid option. Please try again.");
            }
        }
    }

    static void PerformDivision()
    {
        try
        {
            Console.Write("Enter numerator: ");
            var numeratorInput = Console.ReadLine();
            if (!double.TryParse(numeratorInput, out double numerator))
            {
                Log.Error($"Invalid numerator input: {numeratorInput}");
                Console.WriteLine("Invalid number format for numerator.");
                return;
            }

            Console.Write("Enter denominator: ");
            var denominatorInput = Console.ReadLine();
            if (!double.TryParse(denominatorInput, out double denominator))
            {
                Log.Warning($"Invalid denominator input: {denominatorInput}");
                Console.WriteLine("Invalid number format for denominator.");
                return;
            }

            if (denominator == 0)
            {
                Log.Error("Potential division by zero detected");
                Console.WriteLine("Warning: Division by zero is not allowed.");
                return;
            }

            double result = numerator / denominator;
            Console.WriteLine($"Result: {result}");
            Log.Information($"Division operation successful: {numerator} / {denominator} = {result}");
        }
        catch (Exception ex)
        {
            Log.Error($"Error during division operation: {ex.Message}");
            Console.WriteLine("An error occurred during calculation.");
        }
    }
}