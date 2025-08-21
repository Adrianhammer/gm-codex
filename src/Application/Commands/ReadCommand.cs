using gm_codex.Application.Services;
using Spectre.Console;
using Spectre.Console.Cli;

namespace gm_codex.Application.Commands;

public class ReadCommand : Command<ReadSettings>
{
    public static CharacterService? CharacterService { get; set; }

    public override int Execute(CommandContext context, ReadSettings settings)
    {
        if (string.IsNullOrWhiteSpace(settings.Name))
        {
            AnsiConsole.MarkupLine("[red]ERROR:[/]: Name is required.");
            return -1;
        }

        CharacterService?.ReadEntity(settings.Name);
        return 0;
    }
}