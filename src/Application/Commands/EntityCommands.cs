using Spectre.Console;

namespace gm_codex.Application.Commands;

public class EntityCommands
{
    public static string? ReadCharacterCommand()
    {
        
        AnsiConsole.MarkupLine("Type in [bold]Name[/] to retrieve an entity [italic](not case sensitive, but needs to be accurate)[/]");
        var nameInput = Console.ReadLine();

        while (string.IsNullOrEmpty(nameInput))
        {
            Console.WriteLine("Please enter a name.");
            nameInput = Console.ReadLine();
        }
        
        return nameInput;
    }
}