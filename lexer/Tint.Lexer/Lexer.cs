using Tint.Helpers;

namespace Tint.Lexer;

public class Lexer
{
    #region Ctor
    private readonly FileStream _fileStream;
    public readonly string FilePath;
    public Lexer(string filePath)
    {
        _fileStream = FileHelpers.OpenFile(filePath, true);
        FilePath = filePath;
    }
    #endregion
}