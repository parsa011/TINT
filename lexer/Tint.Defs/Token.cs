namespace Tint.Defs;

public record Token(TokenType TokenType, int LineNumber)
{
	public void WriteToken()
	{
		// TODO
		Console.WriteLine("Writing Token...");
	}
}
