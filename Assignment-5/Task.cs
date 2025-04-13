using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
            Console.WriteLine("Enter file paths separated by commas:");
            string input = Console.ReadLine();
            string[] filePaths = input.Split(',')
                                    .Select(f => f.Trim())
                                    .ToArray();

        if (filePaths.Length == 0)
        {
            Console.WriteLine("No files specified.");
            return;
        }

        try
        {
            Task<int>[] lineCountTasks = filePaths.Select(CountLinesInFileAsync).ToArray();
            int[] lineCounts = await Task.WhenAll(lineCountTasks);

            Console.WriteLine("\nResults:");
            for (int i = 0; i < filePaths.Length; i++)
            {
                Console.WriteLine($"{filePaths[i]}: {lineCounts[i]} lines");
            }

            Console.WriteLine($"\nTotal lines : {lineCounts.Sum()}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError: {ex.Message}");
        }
    }

    static async Task<int> CountLinesInFileAsync(string filePath)
    {
        try
        {
            using StreamReader reader = new StreamReader(filePath);
            int lineCount = 0;

            while (await reader.ReadLineAsync() != null)
            {
                lineCount++;
            }

            return lineCount;
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to process '{filePath}': {ex.Message}", ex);
        }
    }
}