class Assignment_2
{
    static void Main()
    {
        // Question 1: Check if a number is prime
        Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
        Console.WriteLine();
        Console.WriteLine("Question 1: Write a program to check whether a given number is prime.");
        Console.WriteLine();
        Console.Write("Enter your Number: ");

        int num_1;
        while (!int.TryParse(Console.ReadLine(), out num_1))
            Console.WriteLine("Enter Integer only!");

        int cnt = 0;
        if (num_1 < 2)
        {
            Console.WriteLine($"Noo! {num_1} is not a Prime Number.");
        }
        else
        {
            for (int i = 2; i < num_1 - 1; i++)
            {
                if (num_1 % i == 0) cnt++;
            }

            if (cnt == 0)
                Console.WriteLine($"Yess! {num_1} is a Prime Number.");
            else
                Console.WriteLine($"Noo! {num_1} is not a Prime Number.");
        }

        // Question 2: Number guessing game
        Console.WriteLine();
        Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
        Console.WriteLine();
        Console.WriteLine("Question 2: Create a program where the user guesses a randomly generated number.");
        Console.WriteLine();

        Random r = new Random();
        int ans_2 = r.Next(0, 11);
        int tries = 3;
        bool win_2 = false;

        Console.WriteLine("Guess a number between 1 to 10");
        Console.WriteLine("( You only have 3 Chances! )");
        for (int y = 0; y < tries; y++)
        {
            Console.Write($"Attempt Number - {y + 1}: ");
            int temp_2;
            while (!int.TryParse(Console.ReadLine(), out temp_2))
                Console.WriteLine("Enter Integer only!");

            if (temp_2 == ans_2)
            {
                win_2 = true;
                Console.WriteLine("Correct! You won the Game!");
                break;
            }

            if (temp_2 < ans_2)
                Console.WriteLine("The Number you guessed is smaller than the answer!");
            else
                Console.WriteLine("The Number you guessed is greater than the answer!");
        }
        Console.WriteLine();
        if (!win_2)
            Console.WriteLine($"Game over! The number was {ans_2}.");

        // Question 3: Student grade calculator
        Console.WriteLine();
        Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
        Console.WriteLine();
        Console.WriteLine("Question 3: Calculate the average and grade of a student.");
        Console.WriteLine();

        double[] marks = new double[3];
        for (int i = 0; i < 3; i++)
        {
            Console.Write($"Enter marks of Subject {i + 1}: ");
            while (!double.TryParse(Console.ReadLine(), out marks[i]))
                Console.WriteLine("Invalid input! Enter numbers only.");
        }

        double average = (marks[0] + marks[1] + marks[2]) / 3;
        string grade;

        if (average >= 90) grade = "A";
        else if (average >= 80) grade = "B";
        else if (average >= 70) grade = "C";
        else if (average >= 60) grade = "D";
        else grade = "F";

        Console.WriteLine($"Your Average is : {average:F2}, Your Grade is : {grade}");

        // Question 4: String reversal
        Console.WriteLine();
        Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
        Console.WriteLine();
        Console.WriteLine("Question 4: Reverse a string entered by the user.");
        Console.WriteLine();

        Console.Write("Enter text: ");
        string text_4 = Console.ReadLine();

        Console.Write("Reversed: ");
        for (int i = text_4.Length - 1; i >= 0; i--)
        {
            Console.Write(text_4[i]);
        }
        Console.WriteLine();

        // Question 5: Fibonacci sequence
        Console.WriteLine();
        Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
        Console.WriteLine();
        Console.WriteLine("Question 5: Display the Fibonacci sequence up to a given number.");
        Console.WriteLine();

        Console.Write("Enter number of terms to be printed : ");
        int num_5;
        while (!int.TryParse(Console.ReadLine(), out num_5) || num_5 <= 0)
            Console.WriteLine("Enter positive integer only!");

        if (num_5 == 1)
        {
            Console.WriteLine("Fibonacci: 0");
        }
        else if (num_5 == 2)
        {
            Console.WriteLine("Fibonacci: 0 1");
        }
        else
        {
            int first = 0, second = 1, temp_5;
            Console.Write("Fibonacci: " + first + " " + second + " ");

            for (int i = 2; i < num_5; i++)
            {
                temp_5 = first + second;
                Console.Write(temp_5 + " ");
                first = second;
                second = temp_5;
            }
            Console.WriteLine();
        }

        // Question 6: Salary calculator
        Console.WriteLine();
        Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
        Console.WriteLine();
        Console.WriteLine("Question 6: Calculate the salary of an employee based on hours worked.");
        Console.WriteLine();

        const double salary = 15.50;
        Console.Write("Enter No.of hours you worked: ");
        double hours;
        while (!double.TryParse(Console.ReadLine(), out hours))
            Console.WriteLine("Enter Integer only!");
        double pay = hours * salary;
        Console.WriteLine($"Your Salary is: ${pay:F2}");

        // Question 7: Multiplication table
        Console.WriteLine();
        Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
        Console.WriteLine();
        Console.WriteLine("Question 7: Display a multiplication table for a number entered by the user.");
        Console.WriteLine();

        Console.Write("Enter Table number : ");
        int num_7;
        while (!int.TryParse(Console.ReadLine(), out num_7))
            Console.WriteLine("Please Enter Integer!");

        Console.WriteLine($"{num_7} Table:");
        for (int i = 1; i < 11; i++)
        {
            int product = num_7 * i;
            Console.WriteLine($"{num_7} x {i} = {product}");
        }

        Console.WriteLine();
        Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
    }
}