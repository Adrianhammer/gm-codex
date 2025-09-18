using gm_codex.Application.Services;
using gm_codex.Presentation.ConsoleUI.Encounters;
using Spectre.Console.Cli;

namespace gm_codex.Application.Commands.Encounters;

public class ListEncounterCommand : Command
{
    private readonly EncounterService  _encounterService;
    public ListEncounterCommand(EncounterService encounterService) => _encounterService = encounterService;

    public override int Execute(CommandContext context)
    {
        var result = _encounterService.ListAllEncounters();
        ReadEncounterUi.ViewAllEncounters(result);
        return result.Success ? 0 : 1;
    }
}