using Tint.Helpers;

namespace Tint.Lexer;

public class Lexer(string filePath)
{
    #region Ctor
    private readonly FileStream _fileStream = FileHelpers.OpenFile(filePath, true);
    public readonly string FilePath = filePath;
    #endregion
}