using gm_codex.Application.Commands.Settings.Encounters;
using gm_codex.Application.ConsoleUI.Entities;
using gm_codex.Application.Services;
using gm_codex.Domain.Enums;
using Spectre.Console;
using Spectre.Console.Cli;

namespace gm_codex.Application.Commands.Encounters;

public class UpdateEncounterParticipantCommand : Command<UpdateEncounterParticipantSetting>
{
    private readonly EncounterParticipantService _participantService;

    public UpdateEncounterParticipantCommand(EncounterParticipantService participantService) =>
        _participantService = participantService;

    public override int Execute(CommandContext context, UpdateEncounterParticipantSetting settings)
    {
        if (string.IsNullOrWhiteSpace(settings.EncounterName))
        {
            AnsiConsole.MarkupLine("[red]ERROR:[/]: Name is required.");
            return -1;
        }
        if (string.IsNullOrWhiteSpace(settings.EntityName))
        {
            AnsiConsole.MarkupLine("[red]ERROR:[/]: Entity name is required.");
            return -1;
        }

        if (string.IsNullOrWhiteSpace(settings.Conditions) && settings.Health is null)
        {
            AnsiConsole.MarkupLine("[red]ERROR:[/]: Health or conditions are required.");
            return -1;
        }
        var result = _participantService.UpdateEncounterParticipant(
            settings.EncounterName,
            settings.EntityName,
            settings.EntityType,
            settings.Health,
            settings.Conditions
        );

        ReadPcUi.ViewConfirmation(result);
        return result.Success ? 0 : -1;
    }
}

public static class UpdateEncounterParticipantCommandExtensions
{
    public static void AddUpdateEncounterParticipantCommand(
        this IConfigurator<CommandSettings> configuration
    )
    {
        configuration
            .AddCommand<UpdateEncounterParticipantCommand>("encounter-participant")
            .WithDescription("Update entities in encounter");
    }
}
