using Spectre.Console;

namespace gm_codex.Application.Commands;

public class EntityCommands
{
    public static string? ReadCharacterCommand()
    {
        AnsiConsole.MarkupLine("Type in [bold]Name[/] to retrieve an entity [italic](not case sensitive, but needs to be accurate)[/]");
        var nameInput = Console.ReadLine();
        
        if (string.IsNullOrEmpty(nameInput))
        {
            Console.WriteLine("Name is empty");
        }
        
        return nameInput;
    }
}