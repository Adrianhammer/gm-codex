using gm_codex.Application.Commands.Settings.Encounters;
using gm_codex.Application.Services;
using gm_codex.Application.ConsoleUI.Encounters;
using Spectre.Console;
using Spectre.Console.Cli;

namespace gm_codex.Application.Commands.Encounters;

public class ReadEncounterCommand : Command<ReadEncounterSettings>
{
    private readonly EncounterService _encounterService;

    public ReadEncounterCommand(EncounterService encounterService) =>
        _encounterService = encounterService;

    public override int Execute(CommandContext context, ReadEncounterSettings settings)
    {
        if (string.IsNullOrWhiteSpace(settings.Name))
        {
            AnsiConsole.MarkupLine("[red]ERROR:[/]: Name is required.");
            return -1;
        }

        var result = _encounterService.ReadEncounter(settings.Name);
        ReadEncounterUi.ViewEncounter(result);
        return result.Success ? 0 : -1;
    }
}

public static class ReadEncounterCommandExtensions
{
    public static void AddReadEncounterCommand(this IConfigurator<CommandSettings> configuration)
    {
        configuration
            .AddCommand<ReadEncounterCommand>("encounter")
            .WithDescription("Get info on one encounter");
    }
}

