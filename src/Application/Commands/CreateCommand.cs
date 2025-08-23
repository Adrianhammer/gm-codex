using gm_codex.Application.Services;
using Spectre.Console;
using Spectre.Console.Cli;

namespace gm_codex.Application.Commands;

public class CreateCommand : Command<CreateSettings>
{
    public static CharacterService? CharacterService { get; set; }

    public override int Execute(CommandContext context, CreateSettings settings)
    {
        if (string.IsNullOrWhiteSpace(settings.Name))
        {
            AnsiConsole.MarkupLine("[red]ERROR:[/]: Name is required.");
            return -1;
        }

        CharacterService?.CreateEntity(settings.Name, settings.EntityType, settings.Race, settings.SubRace, settings.EntityClass, settings.SubClass);
        return 0;
    }
}