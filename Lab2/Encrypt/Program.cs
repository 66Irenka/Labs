using System;
using System.IO;
using System.Text;

class Program
{
    static void Main()
    {
        string file1 = "Iryna1.txt";
        string file2 = "Iryna2.txt";

        if (!File.Exists(file1)) File.Create(file1).Close();
        if (!File.Exists(file2)) File.Create(file2).Close();

        Console.WriteLine($"Відкрийте файл {file1}, введіть текст і збережіть.");
        Console.WriteLine("Після цього натисніть Enter...");
        Console.ReadLine();

        string originalText = File.ReadAllText(file1);
        string encryptedText = EncryptCaesar(originalText);

        File.WriteAllText(file2, encryptedText);

        Console.WriteLine("Текст зашифровано!");
        Console.WriteLine($"Результат у файлі: {file2}");
        Console.WriteLine("Зашифрований текст:");
        Console.WriteLine(encryptedText);
    }

    static string EncryptCaesar(string input)
    {
        StringBuilder sb = new StringBuilder();

        foreach (char c in input)
        {
            if (c == 'Щ') { sb.Append('A'); continue; }
            if (c == 'Ю') { sb.Append('B'); continue; }
            if (c == 'Я') { sb.Append('C'); continue; }
            if (c == 'ь') { sb.Append('d'); continue; }

            if (char.IsLetter(c))
                sb.Append((char)(c + 3));
            else
                sb.Append(c);
        }

        return sb.ToString();
    }
}