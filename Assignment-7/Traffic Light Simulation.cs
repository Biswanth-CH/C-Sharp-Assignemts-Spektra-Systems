using System;

enum TrafficLight
{
    Red,
    Yellow,
    Green
}

class Program
{
    static bool _running = true;

    static void Main(string[] args)
    {
        Console.CursorVisible = false;

        try
        {
            while (_running)
            {
                DisplayHeader();
                ShowLight(TrafficLight.Red);
                if (!_running) break;

                DisplayHeader();
                ShowLight(TrafficLight.Yellow);
                if (!_running) break;

                DisplayHeader();
                ShowLight(TrafficLight.Green);
                if (!_running) break;
            }

            Console.Clear();
            Console.WriteLine("\n\nTraffic light simulation ended\n\n");
        }
        finally
        {
            Console.CursorVisible = true;
        }
    }

    static void DisplayHeader()
    {
        Console.Clear();
        Console.WriteLine("Traffic Light Simulation Statred");
        Console.WriteLine("Press any key to exit...\n");
    }

    static void ShowLight(TrafficLight light)
    {
        Console.WriteLine("+---------+");

        if (light == TrafficLight.Red)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("|  STOP   |");
        }
        else if (light == TrafficLight.Yellow)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("|  READY  |");
        }
        else if (light == TrafficLight.Green)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("|    GO   |");
        }

        Console.ResetColor();
        Console.WriteLine("+---------+");

        DateTime endTime = DateTime.Now.AddSeconds(2);
        while (DateTime.Now < endTime && _running)
        {
            if (Console.KeyAvailable)
            {
                Console.ReadKey();
                _running = false;
                break;
            }
        }
    }
}