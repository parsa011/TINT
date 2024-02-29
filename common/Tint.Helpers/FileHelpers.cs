namespace Tint.Helpers;

public static class FileHelpers
{
    public static string FileNameFromPath(string path)
    {
        int startIndex = 0;
        char pathDelimiter = OsHelpers.PathDelimiter();
        for (int i = path.Length - 1; i >= 0; i--)
        {
            if (path[i] == pathDelimiter)
            {
                startIndex = i + 1;
                break;
            }
        }
        return path[startIndex..path.Length];
    }

    public static FileStream OpenFile(string path, bool shouldExists)
    {
        if (!File.Exists(path))
        {
            if (shouldExists)
            {
                // TODO : i think we need a logger here, to log something like this :
                // [Lexer] : Can't Open File Bla Bla Bla
                Console.WriteLine("File not found");
                Environment.Exit(0);
            }
            return null;
        }
        return new FileStream(path, FileMode.Open);
    }
}