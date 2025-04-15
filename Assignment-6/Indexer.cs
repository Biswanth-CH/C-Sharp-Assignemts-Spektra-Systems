using System;

class StudentGrades
{
    private string[] studentNames;
    private double[] studentGrades;
    private int count;

    public StudentGrades()
    {
        studentNames = new string[100];
        studentGrades = new double[100];
        count = 0;

        AddStudent("Biswnath", 85.5);
        AddStudent("Harshith", 92.0);
        AddStudent("Nageswar Rao", 78.5);
        AddStudent("Hemanth", 88.0);
        AddStudent("Iliaz", 91.5);
    }

    public double this[string studentName]
    {
        get
        {
            int index = FindStudentIndex(studentName);
            if (index >= 0)
                return studentGrades[index];

            throw new KeyNotFoundException($"Student '{studentName}' not found.");
        }
        set
        {
            if (value < 0 || value > 100)
                throw new ArgumentOutOfRangeException("Grade must be between 0 and 100.");

            int index = FindStudentIndex(studentName);
            if (index >= 0)
            {
                studentGrades[index] = value;
            }
            else
            {
                AddStudent(studentName, value);
            }
        }
    }

    private void AddStudent(string name, double grade)
    {
        studentNames[count] = name;
        studentGrades[count] = grade;
        count++;
    }

    private int FindStudentIndex(string studentName)
    {
        for (int i = 0; i < count; i++)
        {
            if (string.Equals(studentNames[i], studentName, StringComparison.OrdinalIgnoreCase))
                return i;
        }
        return -1;
    }

    public void DisplayAllGrades()
    {
        Console.WriteLine("\nStudent Grade Report:");
        Console.WriteLine("---------------------");
        for (int i = 0; i < count; i++)
        {
            Console.WriteLine($"{studentNames[i]}: {studentGrades[i]}");
        }
    }

    public double CalculateClassAverage()
    {
        if (count == 0) return 0;

        double sum = 0;
        for (int i = 0; i < count; i++)
        {
            sum += studentGrades[i];
        }
        return sum / count;
    }

    public int StudentCount => count;
}

class Program
{
    static void Main(string[] args)
    {
        StudentGrades grades = new StudentGrades();

        Console.WriteLine("Student Grades Management System");
        Console.WriteLine("================================");

        while (true)
        {
            Console.Clear();
            Console.WriteLine("\n1. Add/Update Grade");
            Console.WriteLine("2. View Grade");
            Console.WriteLine("3. View All Grades");
            Console.WriteLine("4. View Class Average");
            Console.WriteLine("5. Exit");
            Console.Write("Select an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    AddUpdateGrade(grades);
                    Console.WriteLine("Press any Key to Continue ...");
                    Console.ReadKey();
                    break;
                case "2":
                    Console.Clear();
                    ViewGrade(grades);
                    Console.WriteLine("Press any Key to Continue ...");
                    Console.ReadKey();
                    break;
                case "3":
                    Console.Clear();
                    grades.DisplayAllGrades();
                    Console.WriteLine("Press any Key to Continue ...");
                    Console.ReadKey();
                    break;
                case "4":
                    Console.Clear();
                    Console.WriteLine($"\nClass Average: {grades.CalculateClassAverage():F2}\n");
                    Console.WriteLine("Press any Key to Continue ...");
                    Console.ReadKey();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    static void AddUpdateGrade(StudentGrades grades)
    {
        Console.Write("\nEnter student name: ");
        string name = Console.ReadLine();

        Console.Write("Enter grade (0-100): ");
        if (double.TryParse(Console.ReadLine(), out double grade))
        {
            try
            {
                grades[name] = grade;
                Console.WriteLine($"Grade for {name} updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Invalid grade format. Please enter a number.");
        }
    }

    static void ViewGrade(StudentGrades grades)
    {
        Console.Write("\nEnter student name: ");
        string name = Console.ReadLine();

        try
        {
            Console.WriteLine($"Grade for {name}: {grades[name]}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}