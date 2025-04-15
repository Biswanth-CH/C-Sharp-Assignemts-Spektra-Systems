    using System;
    using System.IO;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    class ParallelFileSearch
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Parallel File Content Search");
            Console.WriteLine("============================");
            Console.WriteLine();

            List<string> filePaths = new List<string>();

            if (filePaths.Count == 0)
            {
                Console.Write("Enter file names (comma separated) : ");
                string input = Console.ReadLine();
                if (input != null)
                {
                    input = input.Trim();

                    if (Directory.Exists(input))
                    {
                        string[] filesInDirectory = Directory.GetFiles(input, "*.txt");
                        foreach (string file in filesInDirectory)
                        {
                            filePaths.Add(file);
                        }
                    }
                    else
                    {
                        string[] fileNames = input.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string file in fileNames)
                        {
                            string trimmedFile = file.Trim();
                            string fullPath;
                            if (trimmedFile.EndsWith(".txt"))
                            {
                                fullPath = trimmedFile;
                            }
                            else
                            {
                                fullPath = trimmedFile + ".txt";
                            }

                            if (File.Exists(fullPath))
                            {
                                filePaths.Add(fullPath);
                            }
                            else
                            {
                                Console.WriteLine($"File not found: {fullPath}");
                            }
                        }
                    }
                }
            }

            if (filePaths.Count == 0)
            {
                Console.WriteLine("No valid files found to search.");
                return;
            }

            Console.Write("Enter keyword to search: ");
            string keywordInput = Console.ReadLine();
            string keyword = keywordInput != null ? keywordInput.Trim() : string.Empty;

            if (string.IsNullOrEmpty(keyword))
            {
                Console.WriteLine("No keyword provided. Exiting.");
                return;
            }

            try
            {
                SearchResult[] results = await SearchKeywordInFilesAsync(filePaths, keyword);

                Console.WriteLine("\nSearch Results:");
                Console.WriteLine("--------------");

                if (results.Length > 0)
                {
                    int totalOccurrences = 0;
                    foreach (SearchResult result in results)
                    {
                        Console.WriteLine($"File: {Path.GetFileName(result.FilePath)}");
                        Console.WriteLine($"Occurrences: {result.Count}");
                        Console.WriteLine();
                        totalOccurrences += result.Count;
                    }

                    Console.WriteLine($"Total occurrences: {totalOccurrences}");
                }
                else
                {
                    Console.WriteLine("Keyword not found in any files.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    
        static async Task<SearchResult[]> SearchKeywordInFilesAsync(List<string> filePaths, string keyword)
        {
            Console.WriteLine($"\nSearching for '{keyword}' in {filePaths.Count} files...");

            Task<SearchResult>[] searchTasks = new Task<SearchResult>[filePaths.Count];
            for (int i = 0; i < filePaths.Count; i++)
            {
                string file = filePaths[i];
                searchTasks[i] = Task.Run(() => SearchInFile(file, keyword));
            }

            return await Task.WhenAll(searchTasks);
        }

        static SearchResult SearchInFile(string filePath, string keyword)
        {
            int count = 0;
            try
            {
                foreach (string line in File.ReadLines(filePath))
                {
                    int index = 0;
                    while ((index = line.IndexOf(keyword, index, StringComparison.OrdinalIgnoreCase)) != -1)
                    {
                        count++;
                        index += keyword.Length;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading {Path.GetFileName(filePath)}: {ex.Message}");
            }
            return new SearchResult(filePath, count);
        }   
    }

    class SearchResult
    {
        public string FilePath { get; }
        public int Count { get; }

        public SearchResult(string filePath, int count)
        {
            FilePath = filePath;
            Count = count;
        }
    }