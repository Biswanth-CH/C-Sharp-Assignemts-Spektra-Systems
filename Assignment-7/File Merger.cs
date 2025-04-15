using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

class FileMerger
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("File Merger");
        Console.WriteLine("===========");

        List<string> inputFiles = GetInputFiles();
        if (inputFiles.Count == 0)
        {
            Console.WriteLine("No files specified. Exiting...");
            return;
        }

        string outputFile = GetOutputFile();
        if (string.IsNullOrWhiteSpace(outputFile))
        {
            Console.WriteLine("Invalid output file. Exiting...");
            return;
        }

        var stopwatch = Stopwatch.StartNew();

        try
        {
            Console.WriteLine("\nStarting merge operation...");

            var readTasks = new List<Task<string>>();
            foreach (var file in inputFiles)
            {
                readTasks.Add(ReadFileAsync(file));
            }

            string[] fileContents = await Task.WhenAll(readTasks);

            string mergedContent = MergeContents(fileContents, inputFiles);

            await WriteFileAsync(outputFile, mergedContent);


            Console.WriteLine("\nMerge completed successfully");
            Console.WriteLine($"Files merged: {inputFiles.Count}");
            Console.WriteLine($"Output file: {outputFile}");
            Console.WriteLine($"Total size: {mergedContent.Length} characters");
            Console.WriteLine($"Time taken: {stopwatch.ElapsedMilliseconds}ms");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nError: {ex.Message}");
        }
    }

    private static List<string> GetInputFiles()
    {
        var files = new List<string>();
        Console.WriteLine("\nEnter input file paths (one per line, blank to finish):");

        while (true)
        {
            Console.Write("> ");
            string input = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(input))
            {
                if (files.Count == 0)
                {
                    Console.WriteLine("Please enter at least one file.");
                    continue;
                }
                break;
            }

            if (!File.Exists(input))
            {
                Console.WriteLine($"File not found: {input}");
                continue;
            }

            files.Add(input);
            Console.WriteLine($"Added: {input}");
        }

        return files;
    }

    private static string GetOutputFile()
    {
        Console.WriteLine("\nEnter output file path:");
        Console.Write("> ");
        string outputFile = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(outputFile))
        {
            return null;
        }

        string directory = Path.GetDirectoryName(outputFile);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Console.WriteLine($"Directory does not exist: {directory}");
            return null;
        }

        return outputFile;
    }

    private static async Task<string> ReadFileAsync(string filePath)
    {
        Console.WriteLine($"Reading {Path.GetFileName(filePath)}...");
        return await File.ReadAllTextAsync(filePath);
    }

    private static async Task WriteFileAsync(string filePath, string content)
    {
        Console.WriteLine($"Writing to {Path.GetFileName(filePath)}...");
        await File.WriteAllTextAsync(filePath, content);
    }

    private static string MergeContents(string[] contents, List<string> filePaths)
    {
        Console.WriteLine("Merging contents...");
        var merged = new System.Text.StringBuilder();

        for (int i = 0; i < contents.Length; i++)
        {
            merged.AppendLine($"// File: {filePaths[i]}");
            merged.AppendLine(contents[i]);
            merged.AppendLine();
        }

        return merged.ToString();
    }
}