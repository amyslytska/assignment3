namespace HashTable;
using System;

internal class Program
{
    public static void Main(string[] args)
    {
        var dictionary = new Dictionary();
        var lines = File.ReadAllLines("C:/Users/annmy/RiderProjects/Hash Table/dictionary.txt");
        int i = 0;
        foreach (string line in lines)
        {
            string[] parts = line.Split(';');
            string key = parts[0].Trim();
            string value = parts[1].Trim();
            dictionary.Add(key, value);
            if (i % 1000 == 0) Console.WriteLine("Number of loaded words " + i);
            i++;
        }
        
        Console.WriteLine("Enter a word:");
        string word = Console.ReadLine();
        string uWord = word.ToUpper();
        string meaning = dictionary.Get(uWord);
        if (meaning != null)
        {
            Console.WriteLine($"'{word}' - : {meaning}");
        }
        else
        {
            Console.WriteLine($"'{word}' was not found.");
        }
    }
}
