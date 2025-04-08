using System;

class Assignment_3
{
    // Helper methods for input validation
    static double GetPositiveDouble()
    {
        while (true)
        {
            if (double.TryParse(Console.ReadLine(), out double result) && result > 0)
                return result;
            Console.Write("Invalid input! Enter a positive number: ");
        }
    }

    static int GetPositiveInt()
    {
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out int result) && result > 0)
                return result;
            Console.Write("Invalid input! Enter a positive integer: ");
        }
    }

    static int GetIntInRange(int min, int max)
    {
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out int result) && result >= min && result <= max)
                return result;
            Console.Write($"Invalid input! Enter a number between {min}-{max}: ");
        }
    }

    static void Main()
    {
        // Question 1: Area Calculation (Abstract Class)
        Console.WriteLine("\n1. Calculate Areas of Shapes");
        Console.WriteLine("---------------------------");

        Console.Write("\n-- Circle --\n\nEnter radius: ");
        double radius = GetPositiveDouble();
        Circle circle = new Circle(radius);
        Console.WriteLine("Area: " + circle.CalculateArea().ToString("F2"));

        Console.Write("\n-- Square --\n\nEnter side length: ");
        double side = GetPositiveDouble();
        Square square = new Square(side);
        Console.WriteLine("Area: " + square.CalculateArea().ToString("F2"));

        Console.Write("\n-- Triangle --\n\nEnter base: ");
        double triangleBase = GetPositiveDouble();
        Console.Write("Enter height: ");
        double height = GetPositiveDouble();
        Triangle triangle = new Triangle(triangleBase, height);
        Console.WriteLine("Area: " + triangle.CalculateArea().ToString("F2"));

        // Question 2: Emplo1yee Details (Encapsulation)
        Console.WriteLine("\n\n2. Employee Details");
        Console.WriteLine("------------------");
        Employee emp = new Employee();

        Console.Write("Enter Employee ID: ");
        emp.ID = GetPositiveInt();

        Console.Write("Enter Name: ");
        emp.Name = Console.ReadLine();

        Console.Write("Enter Salary: ");
        emp.Salary = GetPositiveDouble();

        Console.WriteLine("\n-- Employee Information --");
        emp.DisplayDetails();

        // Question 3: Teacher/Student Details (Inheritance)
        Console.WriteLine("\n\n3. Teacher/Student Details");
        Console.WriteLine("-------------------------");

        Console.Write("Are you entering details for a (1) Teacher or (2) Student? ");
        int personType = GetIntInRange(1, 2);

        if (personType == 1)
        {
            Teacher teacher = new Teacher();
            Console.Write("\nEnter Teacher Name: ");
            teacher.Name = Console.ReadLine();

            Console.Write("Enter Age: ");
            teacher.Age = GetPositiveInt();

            Console.Write("Enter Address: ");
            teacher.Address = Console.ReadLine();

            Console.Write("Enter Subject: ");
            teacher.Subject = Console.ReadLine();

            Console.WriteLine("\n-- Teacher Details --");
            teacher.Display();
        }
        else
        {
            Student student = new Student();
            Console.Write("\nEnter Student Name: ");
            student.Name = Console.ReadLine();

            Console.Write("Enter Age: ");
            student.Age = GetPositiveInt();

            Console.Write("Enter Address: ");
            student.Address = Console.ReadLine();

            Console.Write("Enter Grade: ");
            student.Grade = Console.ReadLine();

            Console.WriteLine("\n-- Student Details --");
            student.Display();
        }

        // Question 4: Payment System (Polymorphism)
        Console.WriteLine("\n\n4. Payment System");
        Console.WriteLine("----------------");

        Console.WriteLine("Payment Methods:");
        Console.WriteLine("1. Digital Wallet");
        Console.WriteLine("2. Bank Transfer");
        Console.WriteLine("3. Credit Card");
        Console.Write("Select payment method (1-3): ");
        int method = GetIntInRange(1, 3);

        Console.Write("Enter payment amount: ");
        double amount = GetPositiveDouble();

        PaymentMethod payment;
        if (method == 1)
            payment = new DigitalWallet();
        else if (method == 2)
            payment = new BankTransfer();
        else
            payment = new CreditCard();

        payment.ProcessPayment(amount);

        // Question 5: ATM System
        Console.WriteLine("\n\n5. ATM System");
        Console.WriteLine("------------");
        ATM atm = new ATM(1000); // Starting with $1000 balance

        Console.WriteLine("\nATM Simulation (Initial balance: $1000.00)");
        Console.Write("Enter amount to withdraw: ");
        double withdrawAmount = GetPositiveDouble();
        atm.Withdraw(withdrawAmount);
        atm.CheckBalance();

        // Question 6: Interactive Seating Arrangement
        Console.WriteLine("\n\n6. Seating Arrangement");
        Console.WriteLine("---------------------");

        Console.Write("Enter number of rows: ");
        int rows = GetPositiveInt();

        int[][] seating = new int[rows][]; // Jagged array for variable seats per row

        for (int i = 0; i < rows; i++)
        {
            Console.Write($"Enter number of seats for row {i + 1}: ");
            int seats = GetPositiveInt();
            seating[i] = new int[seats]; // Initialize each row
        }

        bool continueMarking = true;
        while (continueMarking)
        {
            Console.WriteLine("\nCurrent Seating (Row:Seat) [X=Occupied, O=Empty]:");
            DisplaySeating(seating);

            Console.WriteLine("\nOptions:");
            Console.WriteLine("1. Mark/Unmark Seat");
            Console.WriteLine("2. Finish");
            Console.Write("Select option: ");

            switch (GetIntInRange(1, 2))
            {
                case 1: // Mark/Unmark Seat
                    Console.Write("Enter row number: ");
                    int row = GetIntInRange(1, rows) - 1; // Convert to 0-based index

                    Console.Write($"Enter seat number (1-{seating[row].Length}): ");
                    int seat = GetIntInRange(1, seating[row].Length) - 1;

                    // Toggle seat status (0 ↔ 1)
                    seating[row][seat] = seating[row][seat] == 0 ? 1 : 0;

                    Console.WriteLine($"Seat {seat + 1} in row {row + 1} has been {(seating[row][seat] == 1 ? "occupied" : "vacated")}.");
                    break;

                case 2: // Finish
                    continueMarking = false;
                    break;
            }
        }

        
        Console.WriteLine("\nFinal Seating Arrangement:");
        DisplaySeating(seating);

        static void DisplaySeating(int[][] seating)
        {
            for (int i = 0; i < seating.Length; i++)
            {
                Console.Write($"Row {i + 1}: ");
                for (int j = 0; j < seating[i].Length; j++)
                {
                    Console.Write(seating[i][j] == 0 ? "O " : "X ");
                }
                Console.WriteLine();
            }
        }
    }

    // Shape classes for Question 1
    abstract class Shape
    {
        public abstract double CalculateArea();
    }

    class Circle : Shape
    {
        public double Radius { get; set; }
        public Circle(double radius) { Radius = radius; }
        public override double CalculateArea() { return 3.14 * Radius * Radius; }
    }

    class Square : Shape
    {
        public double Side { get; set; }
        public Square(double side) { Side = side; }
        public override double CalculateArea() { return Side * Side; }
    }

    class Triangle : Shape
    {
        public double Base { get; set; }
        public double Height { get; set; }
        public Triangle(double l, double height)
        {
            Base =l;
            Height = height;
        }
        public override double CalculateArea() { return 0.5 * Base * Height; }
    }

    // Employee class for Question 2
    class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Salary { get; set; }

        public void DisplayDetails()
        {
            Console.WriteLine("ID: " + ID);
            Console.WriteLine("Name: " + Name);
            Console.WriteLine("Salary: $" + Salary.ToString("F2")); 
        }
    }

    // Person classes for Question 3
    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
    }

    class Teacher : Person
    {
        public string Subject { get; set; }
        public void Display()
        {
            Console.WriteLine("Name: " + Name);
            Console.WriteLine("Age: " + Age);
            Console.WriteLine("Address: " + Address);
            Console.WriteLine("Subject: " + Subject);
        }
    }

    class Student : Person
    {
        public string Grade { get; set; }
        public void Display()
        {
            Console.WriteLine("Name: " + Name);
            Console.WriteLine("Age: " + Age);
            Console.WriteLine("Address: " + Address);
            Console.WriteLine("Grade: " + Grade);
        }
    }

    // Payment classes for Question 4
    abstract class PaymentMethod
    {
        public abstract void ProcessPayment(double amount);
    }

    class DigitalWallet : PaymentMethod
    {
        public override void ProcessPayment(double amount)
        {
            Console.WriteLine("\nProcessing $" + amount.ToString("F2") + " via Digital Wallet");
            Console.WriteLine("Payment successful!");
        }
    }

    class BankTransfer : PaymentMethod
    {
        public override void ProcessPayment(double amount)
        {
            Console.WriteLine("\nProcessing $" + amount.ToString("F2") + " via Bank Transfer");
            Console.WriteLine("Transaction completed!");
        }
    }

    class CreditCard : PaymentMethod
    {
        public override void ProcessPayment(double amount)
        {
            Console.WriteLine("\nProcessing $" + amount.ToString("F2") + " via Credit Card");
            Console.WriteLine("Credit card payment authorized!");
        }
    }


    // ATM class for Question 5
    class ATM
    {
        private double balance;

        public ATM(double initialBalance)
        {
            balance = initialBalance;
        }

        public void Withdraw(double amount)
        {
            if (amount > balance)
            {
                Console.WriteLine("Insufficient funds!");
            }
            else if (amount <= 0)
            {
                Console.WriteLine("Amount must be positive!");
            }
            else
            {
                balance -= amount;
                Console.WriteLine("Dispensed $" + amount.ToString("F2"));
            }
        }

        public void CheckBalance()
        {
            Console.WriteLine("Current balance: $" + balance.ToString("F2"));
        }
    }
}