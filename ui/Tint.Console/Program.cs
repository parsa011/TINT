using Spectre.Console;
using Tint.Defs;
using Tint.Helpers;
using Tint.Lexer;

var lexer = new Lexer("../../examples/tokens.tint");

Token t;
do {
    t = lexer.Next();
    if (t.TokenType == TokenType.BadToken)
    {
        Console.WriteLine();
    }
    Console.WriteLine(t.TokenType);
} while (t.TokenType != TokenType.EndOfFile);
Console.WriteLine(lexer.Next().TokenType);
AnsiConsole.MarkupLine(
    $"Lexing '{FileHelpers.FileNameFromPath(lexer.FilePath)}'"
);