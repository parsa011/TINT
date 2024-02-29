using Tint.Helpers;
using Tint.Defs;

namespace Tint.Lexer;

public class Lexer(string filePath)
{
    #region Ctor
    private readonly FileStream _fileStream = FileHelpers.OpenFile(filePath, true);
    public readonly string FilePath = filePath;
    #endregion

    public Token Next()
    {
        while (true)
        {
            int c = _fileStream.ReadByte();
            if (c == -1)
            {
                break;
            }
            Console.Write((char)c);
        }
        Console.WriteLine();
        return new Token(TokenType.String, 1);
    }
}