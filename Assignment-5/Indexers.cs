using System;
using System.Collections.Generic;

public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }

    public Book(string title, string author, int year)
    {
        Title = title;
        Author = author;
        Year = year;
    }
}

public class Library
{
    private List<Book> books = new List<Book>();

    public Book this[int index]
    {
        get
        {
            if (index < 0 || index >= books.Count)
                throw new IndexOutOfRangeException("Book index out of range");
            return books[index];
        }
    }

    public Book this[string title]
    {
        get
        {
            foreach (var book in books)
            {
                if (book.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                    return book;
            }
            throw new KeyNotFoundException($"Book '{title}' not found");
        }
    }

    public void AddBook(Book book) => books.Add(book);

    public void RemoveBook(int index)
    {
        if (index < 0 || index >= books.Count)
            throw new IndexOutOfRangeException("Invalid book index");
        books.RemoveAt(index);
    }

    public void ShowBooks()
    {
        Console.WriteLine("\n=== Library Catalog ===");
        if (books.Count == 0)
        {
            Console.WriteLine("No books in the library.");
            return;
        }

        for (int i = 0; i < books.Count; i++)
        {
            Console.WriteLine($"{i + 1}: {books[i].Title} by {books[i].Author} ({books[i].Year})");
        }
    }

    public bool TryGetBookByTitle(string title, out Book book)
    {
        foreach (var b in books)
        {
            if (b.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
            {
                book = b;
                return true;
            }
        }
        book = null;
        return false;
    }

    public int Count => books.Count;
}

class Program
{
    static void Main(string[] args)
    {
        Library library = new Library();
        bool running = true;

        library.AddBook(new Book("Wings of Fire", "A. P. J. Abdul Kalam", 1999));
        library.AddBook(new Book("It Ends with Us", "Colleen Hoover", 2024));

        while (running)
        {
            Console.Clear();
            Console.WriteLine("\n=== Library Management System ===");
            Console.WriteLine("1. View all books");
            Console.WriteLine("2. Add a book");
            Console.WriteLine("3. Remove a book");
            Console.WriteLine("4. Search by title");
            Console.WriteLine("5. Search by ID");
            Console.WriteLine("6. Exit");
            Console.Write("Select an option: ");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Clear();
                    library.ShowBooks();
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    break;

                case "2":
                    Console.Clear();
                    AddBook(library);
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    break;

                case "3":
                    Console.Clear();
                    RemoveBook(library);
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    break;

                case "4":
                    Console.Clear();
                    SearchByTitle(library);
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    break;

                case "5":
                    Console.Clear();
                    SearchById(library);
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    break;

                case "6":
                    Console.Clear();
                    running = false;
                    Console.WriteLine("Thank you for using the Library Management System!");
                    Console.WriteLine("Press any key to exit...");
                    Console.ReadKey();
                    break;

                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void AddBook(Library library)
    {
        Console.WriteLine("\n=== Add New Book ===");
        Console.Write("Enter book title: ");
        string title = Console.ReadLine();

        Console.Write("Enter author name: ");
        string author = Console.ReadLine();

        Console.Write("Enter publication year: ");
        if (!int.TryParse(Console.ReadLine(), out int year))
        {
            Console.WriteLine("Invalid year. Using current year.");
            year = DateTime.Now.Year;
        }

        library.AddBook(new Book(title, author, year));
        Console.WriteLine("\nBook added successfully!");
    }

    static void RemoveBook(Library library)
    {
        Console.WriteLine("\n=== Remove Book ===");
        library.ShowBooks();
        if (library.Count == 0) return;

        Console.Write("\nEnter ID of book to remove: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            if (id >= 1 && id <= library.Count)
            {
                library.RemoveBook(id - 1);
                Console.WriteLine("\nBook removed successfully!");
            }
            else
            {
                Console.WriteLine($"\nInvalid ID. Please enter between 1 and {library.Count}");
            }
        }
        else
        {
            Console.WriteLine("\nInvalid input. Please enter a number.");
        }
    }

    static void SearchByTitle(Library library)
    {
        Console.WriteLine("\n=== Search by Title ===");
        Console.Write("Enter book title to search: ");
        string title = Console.ReadLine();

        if (library.TryGetBookByTitle(title, out Book found))
        {
            Console.WriteLine($"\nFound book: {found.Title} by {found.Author} ({found.Year})");
        }
        else
        {
            Console.WriteLine($"\nNo book found with title '{title}'");
        }
    }

    static void SearchById(Library library)
    {
        Console.WriteLine("\n=== Search by ID ===");

        if (library.Count == 0)
        {
            Console.WriteLine("No books in the library.");
            return;
        }

        Console.WriteLine($"Enter book ID (1 to {library.Count})");
        Console.Write("or press 'L' to list all books: ");

        string input = Console.ReadLine();

        if (input.Equals("L", StringComparison.OrdinalIgnoreCase))
        {
            library.ShowBooks();
            Console.Write($"\nEnter book ID (1 to {library.Count}): ");
            input = Console.ReadLine();
        }

        if (int.TryParse(input, out int id))
        {
            if (id >= 1 && id <= library.Count)
            {
                Console.WriteLine($"\nBook at ID {id}: {library[id - 1].Title} by {library[id - 1].Author} ({library[id - 1].Year})");
            }
            else
            {
                Console.WriteLine($"\nInvalid ID. Please enter between 1 and {library.Count}");
            }
        }
        else
        {
            Console.WriteLine("\nInvalid input. Please enter a number or 'L' to list books.");
        }
    }
}