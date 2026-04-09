using System.Collections.Generic;
using System.IO;

namespace QuestionsApp;

public static class FileService
{
    public static void WriteLines(string path, List<string> lines)
    {
        File.WriteAllLines(path, lines);
    }

    public static List<string> ReadLines(string path)
    {
        return new List<string>(File.ReadAllLines(path));
    }

    public static void CreateEmptyFile(string path)
    {
        if (!File.Exists(path))
        {
            File.Create(path).Close();
        }
    }
}