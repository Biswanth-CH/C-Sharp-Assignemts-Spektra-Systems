using System;
using System.Collections.Generic;
using System.Linq;

public class Employee
{
    public string Name { get; set; }
    public string Department { get; set; }
    public decimal Salary { get; set; }

    public Employee(string name, string department, decimal salary)
    {
        Name = name;
        Department = department;
        Salary = salary;
    }
}

class Program
{
    static void Main()
    {
        List<Employee> employees = new List<Employee>
        {
            new Employee("Biswanth Ch", "IT", 75000),
            new Employee("Nagesar Rao", "CSE", 65000),
            new Employee("Harshith", "IT", 82000),
            new Employee("Iliaz Khan", "CSE", 58000),
            new Employee("Rajan", "ECE", 72000),
            new Employee("Hemanth", "ECE", 62000)
        };

        Console.WriteLine("Enter department to search (IT/CSE/ECE):");
        string searchDept = Console.ReadLine();

        var deptEmployees = from emp in employees
                            where string.Equals(emp.Department, searchDept, StringComparison.OrdinalIgnoreCase)
                            select emp;

        Console.WriteLine($"\nEmployees in {searchDept} department:");
        foreach (var emp in deptEmployees)
        {
            Console.WriteLine($"{emp.Name} - ${emp.Salary}");
        }

        decimal avgSalary = (from emp in employees
                             select emp.Salary).Average();
        Console.WriteLine($"\nAverage salary across all departments: ${avgSalary:F2}");

        Console.WriteLine("\nSort by:\n1. Name\n2. Salary");
        string sortChoice = Console.ReadLine();

        IEnumerable<Employee> sortedEmployees;
        if (sortChoice == "1")
        {
            sortedEmployees = from emp in employees
                              orderby emp.Name
                              select emp;
        }
        else
        {
            sortedEmployees = from emp in employees
                              orderby emp.Salary
                              select emp;
        }

        Console.WriteLine("\nSorted Employees:");
        foreach (var emp in sortedEmployees)
        {
            Console.WriteLine($"{emp.Name} ({emp.Department}) - ${emp.Salary}");
        }

        var employeesByDept = from emp in employees
                              group emp by emp.Department into deptGroup
                              select deptGroup;

        Console.WriteLine("\nEmployees by Department:");
        foreach (var group in employeesByDept)
        {
            Console.WriteLine($"\n{group.Key} Department:");
            foreach (var emp in group)
            {
                Console.WriteLine($"- {emp.Name}: ${emp.Salary}");
            }
            Console.WriteLine($"Total Employees: {group.Count()}");
            Console.WriteLine($"Average Salary: ${group.Average(emp => emp.Salary):F2}");
        }
    }
}
