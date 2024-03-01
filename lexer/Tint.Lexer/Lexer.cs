using Tint.Helpers;
using Tint.Defs;

namespace Tint.Lexer;

public class Lexer
{
    #region Ctor
    private readonly FileStream _fileStream;
    public readonly string FilePath;
    private readonly Dictionary<char, Func<Token>> _functionTable;
    public Lexer(string filePath)
    {
        FilePath = filePath;
        _fileStream = FileHelpers.OpenFile(filePath, true);
        _functionTable = [];
        for (char c = 'a'; c <= 'z'; c++)
        {
            _functionTable.Add(c, KeyWordToken);
        }
        for (char c = 'A'; c <= 'Z'; c++)
        {
            _functionTable.Add(c, KeyWordToken);
        }
    }
    #endregion

    #region Variables
    char? _pushBackChar = null;
    char? _currentChar = null;
    #endregion

    private char NextChar
    {
        get
        {
            char currentChar;
            if (_pushBackChar != null)
            {
                currentChar = _pushBackChar.Value;
                _pushBackChar = null;
                return currentChar;
            }
            // skip white spaces
            do {
                currentChar = (char)_fileStream.ReadByte();
            } while (char.IsWhiteSpace(currentChar));
            return currentChar;
        }
    }

    private char CurrentChar
    {
        get
        {
            _currentChar ??= NextChar;
            return _currentChar.Value;
        }
    }

    private void PushBack(char ch)
    {
        _pushBackChar = ch;
    }

    /// <summary>
    ///     what about to have a function dictionary, so we can get them by 'CurrentChar'
    /// </summary>
    /// <returns></returns>
    public Token Next()
    {
        var func = _functionTable[CurrentChar];
        Console.WriteLine(func().Type);
        return new Token(TokenType.String, 1);
    }

    public Token KeyWordToken()
    {
        return new Token(TokenType.Keyword, 1);
    }
}