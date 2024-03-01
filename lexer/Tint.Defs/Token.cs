namespace Tint.Defs;

public record Token(TokenType tokenType, int lineNumber)
{
    public readonly TokenType Type = tokenType;
    public readonly int LineNumber = lineNumber;
}