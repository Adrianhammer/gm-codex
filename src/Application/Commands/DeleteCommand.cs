using gm_codex.Application.Services;
using Spectre.Console;
using Spectre.Console.Cli;

namespace gm_codex.Application.Commands;

public class DeleteCommand : Command<DeleteSettings>
{
    public static CharacterService? CharacterService { get; set; }

    public override int Execute(CommandContext context, DeleteSettings settings)
    {
        if (string.IsNullOrWhiteSpace(settings.Name))
        {
            AnsiConsole.MarkupLine("[red]ERROR:[/]: Name is required.");
            return -1;
        }
        
        CharacterService?.DeleteEntity(settings.Name);
        return 0;
    }
}