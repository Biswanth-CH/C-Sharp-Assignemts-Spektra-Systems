using System;


namespace ConsoleApp1
{
    class Assignment_1
    {
        static void Main()
        {
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine();

            Console.WriteLine("Question 1: Program to Count Number of Words in a String.");
            Console.WriteLine();

            Console.WriteLine("Enter your String:");
            string input_1 = Console.ReadLine();
            int counter = 0;
            bool temp = false;
            for (int i = 0; i < input_1.Length; i++)
            {
                char c = input_1[i];
                if (char.IsWhiteSpace(c))
                {
                    temp = false;
                }
                else if (!temp) 
                {
                    temp = true;
                    counter++;
                }
            }
            Console.WriteLine();

            Console.WriteLine($"Number of words: {counter}");
            Console.WriteLine();










            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine();

            Console.WriteLine("Question 2: Get Time Zone Data on the Windows platform.");
            Console.WriteLine();

            DateTime ans_2 = DateTime.Now;
            TimeZoneInfo tz = TimeZoneInfo.Local;
            Console.WriteLine($"Current local time in my System is : {ans_2}");
            Console.WriteLine($"Current Time Zone in my system is : {tz}");
            Console.WriteLine();










            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine();

            Console.WriteLine("Question 3: Program to finds the average of 3 numbers in C#");
            Console.WriteLine();

            int n1, n2, n3;
            Console.WriteLine("Enter three numbers: ");
            n1 = int.Parse(Console.ReadLine());
            n2 = int.Parse(Console.ReadLine());
            n3 = int.Parse(Console.ReadLine());
            Console.WriteLine($"Average of {n1}, {n2}, {n3} is {(n1 + n2 + n3) / 3}");
            Console.WriteLine();










            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine();

            Console.WriteLine("Question 4: Program To Calculate the Simple Interest in C#");
            Console.WriteLine();
            Console.WriteLine("Enter Principal amount:");
            float principal = float.Parse(Console.ReadLine());
            Console.WriteLine("Enter Rate of interest:");
            float rate = float.Parse(Console.ReadLine());
            Console.WriteLine("Enter Time period in years:");
            float time = float.Parse(Console.ReadLine());
            float simpleInterest = (principal * rate * time) / 100;
            Console.WriteLine($"Simple Interest: {simpleInterest}");
            Console.WriteLine();









            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine();

            Console.WriteLine("Question 5: Finding the biggest of three numbers in C#");
            Console.WriteLine();

            int[] arr = new int[3];
            for (int i = 0; i < 3; i++)
            {
                arr[i] = int.Parse(Console.ReadLine());
            }
            int l = arr[0];
            for (int i = 0; i < 3; i++)
            {
                if (arr[i] > l)
                {
                    l = arr[i];
                }
            }
            Console.WriteLine($"Largest number among {arr[0]}, {arr[1]} and {arr[2]} is {(l)}");

            Console.WriteLine();
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");

        }
    }
}
