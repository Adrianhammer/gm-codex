using gm_codex.Application.Commands.Settings.Encounters;
using gm_codex.Application.ConsoleUI.Entities;
using gm_codex.Application.Services;
using Spectre.Console;
using Spectre.Console.Cli;

namespace gm_codex.Application.Commands.Encounters;

public class EncounterParticipantCommand : Command<EncounterParticipantSetting>
{
    private readonly EncounterParticipantService _participantService;

    public EncounterParticipantCommand(EncounterParticipantService participantService) =>
        _participantService = participantService;

    public override int Execute(CommandContext context, EncounterParticipantSetting settings)
    {
        if (string.IsNullOrWhiteSpace(settings.EncounterName))
        {
            AnsiConsole.MarkupLine("[red]ERROR:[/]: Name is required.");
            return -1;
        }

        if (settings.Npc.Length == 0)
        {
            AnsiConsole.MarkupLine("[red]ERROR:[/]: At least one NPC is required.");
            return -1;
        }
        if (settings.Pc.Length == 0)
        {
            AnsiConsole.MarkupLine("[red]ERROR:[/]: At least one PC is required.");
            return -1;
        }

        var result = _participantService.AddParticipantsToEncounter(
            settings.EncounterName,
            settings.Npc,
            settings.Pc
        );
        ReadPcUi.ViewConfirmation(result);
        return result.Success ? 0 : -1;
    }
}

public static class EncounterParticipantCommandExtensions
{
    public static void AddCreateEncounterParticipantCommand(
        this IConfigurator<CommandSettings> configuration
    )
    {
        configuration
            .AddCommand<EncounterParticipantCommand>("encounter-participant")
            .WithDescription("Add entities to encounter");
    }
}
