    using System;
    using System.Collections.Generic;

    public record Book(
        string ISBN,
        string Title,
        string Author,
        string Genre,
        bool IsCheckedOut,
        DateTime? DueDate
    );

    public class Library
    {
        private List<Book> _books = new List<Book>
        {
            new Book(
                ISBN: "978-8173711469",
                Title: "Wings of Fire",
                Author: "A.P.J. Abdul Kalam",
                Genre: "Autobiography",
                IsCheckedOut: false,
                DueDate: null
            ),
            new Book(
                ISBN: "978-1501110368",
                Title: "It Ends With Us",
                Author: "Colleen Hoover",
                Genre: "Contemporary Fiction",
                IsCheckedOut: false,
                DueDate: null
            ),
            new Book(
                ISBN: "978-0486292731",
                Title: "Gulliver's Travels",
                Author: "Jonathan Swift",
                Genre: "Classic Literature",
                IsCheckedOut: false,
                DueDate: null
            )
        };

        public Book CheckOutBook(string isbn, int daysUntilDue)
        {
            int index = -1;
            for (int i = 0; i < _books.Count; i++)
            {
                if (_books[i].ISBN == isbn)
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
            {
                throw new ArgumentException("Book not found.");
            }

            Book book = _books[index];

            if (book.IsCheckedOut)
            {
                throw new InvalidOperationException("Book already checked out.");
            }

            Book updatedBook = book with
            {
                IsCheckedOut = true,
                DueDate = DateTime.Today.AddDays(daysUntilDue)
            };

            _books[index] = updatedBook;
            return updatedBook;
        }

        public Book ReturnBook(string isbn)
        {
            int index = -1;
            for (int i = 0; i < _books.Count; i++)
            {
                if (_books[i].ISBN == isbn)
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
            {
                throw new ArgumentException("Book not found.");
            }

            Book book = _books[index];

            if (!book.IsCheckedOut)
            {
                throw new InvalidOperationException("Book wasn't checked out.");
            }

            Book updatedBook = book with
            {
                IsCheckedOut = false,
                DueDate = null
            };

            _books[index] = updatedBook;
            return updatedBook;
        }

        public void DisplayBooks()
        {
            Console.WriteLine("\nLibrary Books:");
            Console.WriteLine("-----------------------------------------------------------------------");
            Console.WriteLine("| ISBN           | Title               | Status                       |");
            Console.WriteLine("-----------------------------------------------------------------------");

            for (int i = 0; i < _books.Count; i++)
            {
                Book book = _books[i];
                string status;

                if (book.IsCheckedOut)
                {
                    status = "Checked Out (Due: " + book.DueDate?.ToString("d") + ")";
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else
                {
                    status = "Available";
                    Console.ForegroundColor = ConsoleColor.Green;
                }

                Console.Write($"| {book.ISBN,-14} | {book.Title,-19} | ");
                Console.Write($"{status,-29}");
                Console.ResetColor();
                Console.WriteLine("|");
            }

            Console.WriteLine("-----------------------------------------------------------------------");
        }
    }

    public class Program
    {
        public static void Main()
        {
            Library library = new Library();

            while (true)
            {
                Console.Clear();
                library.DisplayBooks();

                Console.WriteLine("\n1. Check out book");
                Console.WriteLine("2. Return book");
                Console.WriteLine("3. Exit");
                Console.Write("Select option: ");

                string choice = Console.ReadLine();

                try
                {
                    if (choice == "1")
                    {
                        CheckOutProcess(library);
                    }
                    else if (choice == "2")
                    {
                        ReturnProcess(library);
                    }
                    else if (choice == "3")
                    {
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Invalid option");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }

        static void CheckOutProcess(Library library)
        {
            Console.Write("\nEnter ISBN to checkout: ");
            string isbn = Console.ReadLine();

            Console.Write("Enter days until due: ");
            string daysInput = Console.ReadLine();
            int days;
            if (!int.TryParse(daysInput, out days) || days <= 0)
            {
                throw new ArgumentException("Invalid days value");
            }

            Book book = library.CheckOutBook(isbn, days);
            Console.WriteLine($"Checked out: {book.Title} (Due: {book.DueDate?.ToString("d")})");
        }

        static void ReturnProcess(Library library)
        {
            Console.Write("\nEnter ISBN to return: ");
            string isbn = Console.ReadLine();

            Book book = library.ReturnBook(isbn);
            Console.WriteLine($"Returned: {book.Title}");
        }
    }
