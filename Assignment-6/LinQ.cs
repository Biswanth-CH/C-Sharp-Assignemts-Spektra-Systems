using System;
using System.Collections.Generic;
using System.Linq;

class Student
{
    public string Name { get; set; }
    public string Subject { get; set; }
    public int Marks { get; set; }

    public Student(string name, string subject, int marks)
    {
        Name = name;
        Subject = subject;
        Marks = marks;
    }
}

class Program
{
    static List<Student> students = new List<Student>
    {
        new Student("Biswnath", "Mathematics", 85),
        new Student("Biswnath", "Physics", 78),
        new Student("Harshith", "Mathematics", 92),
        new Student("Harshith", "Physics", 88),
        new Student("Nageswar Rao", "Mathematics", 78),
        new Student("Nageswar Rao", "Physics", 82),
        new Student("Hemanth", "Mathematics", 88),
        new Student("Hemanth", "Physics", 85),
        new Student("Iliaz", "Mathematics", 91),
        new Student("Iliaz", "Physics", 89),
        new Student("Rahul", "Chemistry", 75),
        new Student("Priya", "Chemistry", 82)
    };

    static void Main()
    {
        bool exit = false;

        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("Student Exam Results Analysis");
            Console.WriteLine("============================\n");
            Console.WriteLine("1. Find high-scoring students (above threshold)");
            Console.WriteLine("2. Show average marks per subject");
            Console.WriteLine("3. Show students sorted by total marks");
            Console.WriteLine("4. Show highest scorers per subject");
            Console.WriteLine("5. Exit");
            Console.Write("\nEnter your choice (1-5): ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ShowHighScorers();
                    break;
                case "2":
                    ShowSubjectAverages();
                    break;
                case "3":
                    ShowStudentsByTotalMarks();
                    break;
                case "4":
                    ShowTopScorersPerSubject();
                    break;
                case "5":
                    exit = true;
                    continue;
                default:
                    Console.WriteLine("\nInvalid choice. Please try again.");
                    WaitForKey();
                    break;
            }
        }
    }

    static void ShowHighScorers()
    {
        Console.Clear();
        Console.WriteLine("Find High-Scoring Students");
        Console.WriteLine("=========================\n");

        Console.Write("Enter minimum marks threshold (default 85): ");
        if (!int.TryParse(Console.ReadLine(), out int threshold))
        {
            threshold = 85;
        }

        var highScorers = students.Where(s => s.Marks > threshold)
                                 .OrderByDescending(s => s.Marks);

        Console.WriteLine($"\nStudents scoring above {threshold}:\n");
        foreach (var student in highScorers)
        {
            Console.WriteLine($"{student.Name,-15} {student.Subject,-12} {student.Marks}");
        }

        WaitForKey();
    }

    static void ShowSubjectAverages()
    {
        Console.Clear();
        Console.WriteLine("Average Marks Per Subject");
        Console.WriteLine("=========================\n");

        var subjectAverages = students.GroupBy(s => s.Subject)
                                     .Select(g => new {
                                         Subject = g.Key,
                                         Average = g.Average(s => s.Marks)
                                     })
                                     .OrderByDescending(s => s.Average);

        foreach (var subject in subjectAverages)
        {
            Console.WriteLine($"{subject.Subject,-12}: {subject.Average:F1}");
        }

        WaitForKey();
    }

    static void ShowStudentsByTotalMarks()
    {
        Console.Clear();
        Console.WriteLine("Students Sorted by Total Marks");
        Console.WriteLine("=============================\n");

        var studentTotals = students.GroupBy(s => s.Name)
                                   .Select(g => new {
                                       Name = g.Key,
                                       TotalMarks = g.Sum(s => s.Marks)
                                   })
                                   .OrderByDescending(s => s.TotalMarks);

        Console.WriteLine($"{"Name",-15} Total Marks\n");
        foreach (var student in studentTotals)
        {
            Console.WriteLine($"{student.Name,-15} {student.TotalMarks}");
        }

        WaitForKey();
    }

    static void ShowTopScorersPerSubject()
    {
        Console.Clear();
        Console.WriteLine("Highest Scorers Per Subject");
        Console.WriteLine("==========================\n");

        var topScorers = students.GroupBy(s => s.Subject)
                                .Select(g => g.OrderByDescending(s => s.Marks)
                                             .First())
                                .OrderBy(s => s.Subject);

        Console.WriteLine($"{"Subject",-12} {"Name",-15} Marks\n");
        foreach (var top in topScorers)
        {
            Console.WriteLine($"{top.Subject,-12} {top.Name,-15} {top.Marks}");
        }

        WaitForKey();
    }

    static void WaitForKey()
    {
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
}