using Spectre.Console;
using Tint.Helpers;
using Tint.Lexer;

var lexer = new Lexer(args[0]!);

AnsiConsole.Status()
    .Start("Compiling...", ctx => 
    {
        lexer.Next();
        AnsiConsole.MarkupLine(
            $"Lexing '{FileHelpers.FileNameFromPath(lexer.FilePath)}'"
        );
    });
