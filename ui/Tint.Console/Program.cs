using Spectre.Console;
using Tint.Defs;
using Tint.Helpers;
using Tint.Lexer;

var lexer = new Lexer(args[0]!);

AnsiConsole.Status()
    .Start("Compiling...", ctx => 
    {
        Token t;
        do {
            t = lexer.Next();
            t.WriteToken();
        } while (t.TokenType != TokenType.EndOfFile);
        Console.WriteLine(lexer.Next().TokenType);
        AnsiConsole.MarkupLine(
            $"Lexing '{FileHelpers.FileNameFromPath(lexer.FilePath)}'"
        );
    });
