using gm_codex.Application.Commands.Settings.Encounters;
using gm_codex.Application.ConsoleUI.Entities;
using gm_codex.Application.Services;
using Spectre.Console;
using Spectre.Console.Cli;

namespace gm_codex.Application.Commands.Encounters;

public class UpdateEncounterCommand : Command<UpdateEncounterSettings>
{
    private readonly EncounterService _encounterService;

    public UpdateEncounterCommand(EncounterService encounterService) =>
        _encounterService = encounterService;

    public override int Execute(CommandContext context, UpdateEncounterSettings settings)
    {
        if (string.IsNullOrWhiteSpace(settings.Name))
        {
            AnsiConsole.MarkupLine("[red]ERROR:[/]: Name is required.");
            return -1;
        }

        var result = _encounterService.UpdateEncounter(settings.Name, settings.Description);
        ReadPcUi.ViewConfirmation(result);
        return result.Success ? 0 : -1;
    }
}

public static class UpdateEncounterCommandExtensions
{
    public static void AddUpdateEncounterCommand(this IConfigurator<CommandSettings> configuration)
    {
        configuration
            .AddCommand<UpdateEncounterCommand>("encounter")
            .WithDescription("Update one encounter");
    }
}
