using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class User
{
    public string Name { get; set; }
    public int Age { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        List<User> users = new List<User>
        {
            new User { Name = "Biswanth", Age = 20 },
            new User { Name = "Sanjana", Age = 21 },
            new User { Name = "Manoj", Age = 23 }
        };

        string json = JsonConvert.SerializeObject(users, Formatting.Indented);
        Console.WriteLine("Json Data :\n");
        Console.WriteLine(json);

        List<User> users_2 = JsonConvert.DeserializeObject<List<User>>(json);

        Console.WriteLine("\n       User Details");
        Console.WriteLine("--------------------------");

        for (int i = 0; i < users_2.Count; i++)
        {
            Console.WriteLine($"Name: {users_2[i].Name} , Age: {users_2[i].Age}");
        }
    }
}