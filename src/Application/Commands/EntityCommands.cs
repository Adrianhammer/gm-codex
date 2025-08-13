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
    
    public static string DeleteEntityCommand()
    {
        AnsiConsole.MarkupLine("Which entity would you like to delete? Type in [bold]name[/] to delete an entity.");
        var entity =  Console.ReadLine();

        while (string.IsNullOrEmpty(entity))
        {
            Console.WriteLine("You need to enter the name of the entity you would like to delete.");
            entity = Console.ReadLine();
        }

        return entity;
    }
}