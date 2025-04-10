
class Employee
{
    public string EmployeeID;
    public string Name;
    public string Department;
    public string Salary;
}

class Program
{
    static List<Employee> employees = new List<Employee>();
    static string filePath = "employees.csv";

    static void Main(string[] args)
    {
        LoadEmployees();

        bool running = true;
        while (running)
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════╗");
            Console.WriteLine("║ Employee Management System   ║");
            Console.WriteLine("╠══════════════════════════════╣");
            Console.WriteLine("║ 1. View Employees            ║");
            Console.WriteLine("║ 2. Add New Employee          ║");
            Console.WriteLine("║ 3. Update Employee           ║");
            Console.WriteLine("║ 4. Delete Employee           ║");
            Console.WriteLine("║ 5. Save Changes              ║");
            Console.WriteLine("║ 6. Exit                      ║");
            Console.WriteLine("╚══════════════════════════════╝");
            Console.Write("\nSelect an option (1-6): ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    DisplayEmployees();
                    break;
                case "2":
                    AddEmployee();
                    break;
                case "3":
                    UpdateEmployee();
                    break;
                case "4":
                    DeleteEmployee();
                    break;
                case "5":
                    SaveEmployees();
                    break;
                case "6":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Press any key to continue...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void LoadEmployees()
    {
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            for (int i = 1; i < lines.Length; i++) 
            {
                string[] parts = lines[i].Split(',');
                if (parts.Length == 4)
                {
                    Employee emp = new Employee();
                    emp.EmployeeID = parts[0];
                    emp.Name = parts[1];
                    emp.Department = parts[2];
                    emp.Salary = parts[3];
                    employees.Add(emp);
                }
            }
        }
        else
        {
            File.WriteAllText(filePath, "EmployeeID,Name,Department,Salary\n");
        }
    }

    static void DisplayEmployees()
    {
        Console.Clear();
        Console.WriteLine("Employee List:");
        Console.WriteLine("----------------------------");

        if (employees.Count == 0)
        {
            Console.WriteLine("No employees found");
        }
        else
        {
            for (int i = 0; i < employees.Count; i++)
            {
                Console.WriteLine("ID: " + employees[i].EmployeeID);
                Console.WriteLine("Name: " + employees[i].Name);
                Console.WriteLine("Department: " + employees[i].Department);
                Console.WriteLine("Salary: " + employees[i].Salary);
                Console.WriteLine("----------------------------");
            }
        }

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    static void AddEmployee()
    {
        Console.Clear();
        Employee newEmp = new Employee();

        Console.Write("Enter Employee ID: ");
        newEmp.EmployeeID = Console.ReadLine();

        Console.Write("Enter Name: ");
        newEmp.Name = Console.ReadLine();

        Console.Write("Enter Department: ");
        newEmp.Department = Console.ReadLine();

        Console.Write("Enter Salary: ");
        newEmp.Salary = Console.ReadLine();

        employees.Add(newEmp);
        Console.WriteLine("\nEmployee added successfully!");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    static void UpdateEmployee()
    {
        Console.Clear();
        Console.Write("Enter Employee ID to update: ");
        string id = Console.ReadLine();

        Employee empToUpdate = null;
        for (int i = 0; i < employees.Count; i++)
        {
            if (employees[i].EmployeeID == id)
            {
                empToUpdate = employees[i];
                break;
            }
        }

        if (empToUpdate == null)
        {
            Console.WriteLine("Employee not found!");
        }
        else
        {
            Console.WriteLine("\nCurrent Details:");
            Console.WriteLine("Name: " + empToUpdate.Name);
            Console.WriteLine("Department: " + empToUpdate.Department);
            Console.WriteLine("Salary: " + empToUpdate.Salary);

            Console.WriteLine("\nEnter new details (leave blank to keep current):");

            Console.Write("New Department: ");
            string newDept = Console.ReadLine();
            if (!string.IsNullOrEmpty(newDept))
                empToUpdate.Department = newDept;

            Console.Write("New Salary: ");
            string newSalary = Console.ReadLine();
            if (!string.IsNullOrEmpty(newSalary))
                empToUpdate.Salary = newSalary;

            Console.WriteLine("\nEmployee updated successfully!");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    static void DeleteEmployee()
    {
        Console.Clear();
        Console.Write("Enter Employee ID to delete: ");
        string id = Console.ReadLine();

        bool found = false;
        for (int i = 0; i < employees.Count; i++)
        {
            if (employees[i].EmployeeID == id)
            {
                Console.WriteLine("\nDelete this employee?");
                Console.WriteLine("Name: " + employees[i].Name);
                Console.WriteLine("(Y/N)? ");
                if (Console.ReadLine().ToUpper() == "Y")
                {
                    employees.RemoveAt(i);//remove in list
                    Console.WriteLine("Employee deleted!");
                }
                else
                {
                    Console.WriteLine("Deletion cancelled.");
                }
                found = true;
                break;
            }
        }

        if (!found)
        {
            Console.WriteLine("Employee not found!");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    static void SaveEmployees()
    {
        List<string> lines = new List<string>();
        lines.Add("EmployeeID,Name,Department,Salary");

        for (int i = 0; i < employees.Count; i++)
        {
            lines.Add(employees[i].EmployeeID + "," + employees[i].Name + "," + employees[i].Department + "," + employees[i].Salary);
        }

        File.WriteAllLines(filePath, lines);
        Console.WriteLine("Changes saved successfully!");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}