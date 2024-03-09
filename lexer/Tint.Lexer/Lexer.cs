using Tint.Helpers;
using Tint.Defs;

namespace Tint.Lexer;

public class Lexer
{
    #region Ctor
    private readonly FileStream _fileStream;
    public readonly string FilePath;
    private readonly Dictionary<string, Func<Token>> _functionTable;
    public Lexer(string filePath)
    {
        FilePath = filePath;
        _fileStream = FileHelpers.OpenFile(filePath, true);
        _functionTable = [];
		// there should be a little note :
		//   if this we shold lex token like '=' and '==' in one method
		//   in separate way (method) it's hard to detect, but in this way
		//   we just need to see next character, if it was not token, push back
		//   that character
        for (char c = 'A'; c <= 'Z'; c++)
        {
            _functionTable.Add(c.ToString(), KeyWordToken);
			_functionTable.Add((c + ('a' - 'A')).ToString(), KeyWordToken);
		}
		for (char c = '0'; c <= '9'; c++)
		{
			_functionTable.Add(c.ToString(), KeyWordToken);
		}
		_functionTable.Add(".", DotToken);
		_functionTable.Add("+", DotToken);
		_functionTable.Add("-", DotToken);
		_functionTable.Add("/", DotToken);
		_functionTable.Add("%", DotToken);
		_functionTable.Add("*", DotToken);
		_functionTable.Add("\"", DotToken);
		_functionTable.Add("'", DotToken);
		_functionTable.Add("&", DotToken);
		_functionTable.Add("&&", DotToken);
		_functionTable.Add("|", DotToken);
		_functionTable.Add("||", DotToken);
		_functionTable.Add("^", DotToken);
		_functionTable.Add("=", DotToken);
		_functionTable.Add("==", DotToken);
		_functionTable.Add("+=", DotToken);
		_functionTable.Add("-=", DotToken);
		_functionTable.Add("*=", DotToken);
		_functionTable.Add("/=", DotToken);
		_functionTable.Add("%=", DotToken);
		_functionTable.Add("<", DotToken);
		_functionTable.Add("<=", DotToken);
		_functionTable.Add(">", DotToken);
		_functionTable.Add(">=", DotToken);
		_functionTable.Add("!", DotToken);
		_functionTable.Add("(", DotToken);
		_functionTable.Add(")", DotToken);
		_functionTable.Add("[", DotToken);
		_functionTable.Add("]", DotToken);
		_functionTable.Add("{", DotToken);
		_functionTable.Add("}", DotToken);
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
	/// 	return Token from stream, by detecting the character and calling proper function to generate new token
	/// 	idk if this comment is usefull or no, but i'm doing my best to explain
    /// </summary>
    /// <returns></returns>
    public Token Next()
    {
        return ReadToken(CurrentChar.ToString());
    }

    private Token ReadToken(string c)
    {
        if (!_functionTable.Any(a => a.Key == c))
        {
            return BadToken();
        }
        return _functionTable[c].Invoke();
    }

    private Token BadToken()
    {
        return new Token(TokenType.BadToken, 1);
    }

    private Token KeyWordToken()
    {
        return new Token(TokenType.Keyword, 1);
    }

	private Token DotToken()
	{
		return new Token(TokenType.Dot, 1);
	}
}
