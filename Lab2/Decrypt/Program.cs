using System;
using System.IO;
using System.Text;

class Program
{
    static void Main()
    {
        string file2 = "Iryna2.txt";
        string file3 = "Iryna3.txt";

        if (File.Exists(file2))
        {
            Console.WriteLine($"Читання файлу {file2}...");

            string encryptedText = File.ReadAllText(file2);
            string decryptedText = DecryptCaesar(encryptedText);

            File.WriteAllText(file3, decryptedText);

            Console.WriteLine("Текст розшифровано!");
            Console.WriteLine($"Результат у файлі: {file3}");
            Console.WriteLine("Розшифрований текст:");
            Console.WriteLine(decryptedText);
        }
        else
        {
            Console.WriteLine("Файл Iryna2.txt не знайдено.");
        }
    }

    static string DecryptCaesar(string input)
    {
        StringBuilder sb = new StringBuilder();

        foreach (char c in input)
        {
            if (c == 'A') { sb.Append('Щ'); continue; }
            if (c == 'B') { sb.Append('Ю'); continue; }
            if (c == 'C') { sb.Append('Я'); continue; }
            if (c == 'd') { sb.Append('ь'); continue; }

            if (char.IsLetter(c))
                sb.Append((char)(c - 3));
            else
                sb.Append(c);
        }

        return sb.ToString();
    }
}