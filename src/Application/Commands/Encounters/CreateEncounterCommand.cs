using gm_codex.Application.Commands.Settings.Encounters;
using gm_codex.Application.Services;
using gm_codex.Presentation.ConsoleUI;
using Spectre.Console;
using Spectre.Console.Cli;

namespace gm_codex.Application.Commands.Encounters;

public class CreateEncounterCommand : Command<CreateEncounterSettings>
{
    private readonly EncounterService _encounterService;
    
    public CreateEncounterCommand(EncounterService encounterService) => _encounterService = encounterService;
    
    public override int Execute(CommandContext context, CreateEncounterSettings settings)
    {
        if (string.IsNullOrWhiteSpace(settings.Name))
        {
            AnsiConsole.MarkupLine("[red]ERROR:[/]: Name is required.");
            return -1;
        }

        var result = _encounterService.CreateEncounter(settings.Name, settings.Description);
        ReadPcUi.ViewConfirmation(result);
        return result.Success ? 0 : -1;
    }
}