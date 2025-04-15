using System;

public class BinarySearchExample
{
    public static int BinarySearch<T>(T[] sortedArray, T item) where T : IComparable<T>
    {
        int low = 0;
        int high = sortedArray.Length - 1;

        while (low <= high)
        {
            int mid = low + (high - low) / 2;
            int comparison = sortedArray[mid].CompareTo(item);

            if (comparison == 0)
                return mid;
            else if (comparison < 0)
                low = mid + 1;
            else
                high = mid - 1;
        }
        return -1;
    }

    public static bool IsArraySorted<T>(T[] array) where T : IComparable<T>
    {
        for (int i = 0; i < array.Length - 1; i++)
        {
            if (array[i].CompareTo(array[i + 1]) > 0)
                return false;
        }
        return true;
    }

    public static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("==== BINARY SEARCH PROGRAM ====\n");

            try
            {
                Console.WriteLine("Enter array elements separated by commas (or type 'exit' to quit):");
                string input = Console.ReadLine();

                if (input.ToLower() == "exit")
                    break;

                Console.WriteLine("\nChoose array type:");
                Console.WriteLine("1. Integer");
                Console.WriteLine("2. String");
                Console.Write("Your choice (1 or 2): ");
                string typeChoice = Console.ReadLine();

                if (typeChoice == "1")
                {
                    string[] parts = input.Split(',');
                    int[] numbers = new int[parts.Length];
                    for (int i = 0; i < parts.Length; i++)
                        numbers[i] = int.Parse(parts[i].Trim());

                    if (!IsArraySorted(numbers))
                        throw new Exception("Array is not sorted. Binary search requires sorted input.");

                    Console.WriteLine("\nEnter number to search:");
                    int target = int.Parse(Console.ReadLine());
                    int result = BinarySearch(numbers, target);

                    Console.ForegroundColor = result >= 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    Console.WriteLine(result >= 0 ? $"Found at index: {result}" : "Not found");
                }
                else if (typeChoice == "2")
                {
                    string[] parts = input.Split(',');
                    string[] words = new string[parts.Length];
                    for (int i = 0; i < parts.Length; i++)
                        words[i] = parts[i].Trim();

                    if (!IsArraySorted(words))
                        throw new Exception("Array is not sorted. Binary search requires sorted input.");

                    Console.WriteLine("\nEnter word to search:");
                    string target = Console.ReadLine().Trim();
                    int result = BinarySearch(words, target);

                    Console.ForegroundColor = result >= 0 ? ConsoleColor.Green : ConsoleColor.Red;
                    Console.WriteLine(result >= 0 ? $"Found at index: {result}" : "Not found");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid choice.");
                }
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid number format.");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ResetColor();
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }

        Console.WriteLine("\nProgram exited.");
    }
}
