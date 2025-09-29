using gm_codex.Application.Commands.Settings.Party;
using gm_codex.Application.Services;
using gm_codex.Presentation.ConsoleUI.Entities;
using Spectre.Console;
using Spectre.Console.Cli;

namespace gm_codex.Application.Commands.Party;

public class AddPartyMemberCommand : Command<AddPartyMemberSettings>
{
    private readonly PartyService _partyService;

    public AddPartyMemberCommand(PartyService partyService) => _partyService = partyService;

    public override int Execute(CommandContext context, AddPartyMemberSettings settings)
    {
        if (string.IsNullOrWhiteSpace(settings.Name) && string.IsNullOrWhiteSpace(settings.Party))
        {
            AnsiConsole.MarkupLine("[red]ERROR:[/]: Name is required.");
            return -1;
        }
        
        var result = _partyService.AddPcToParty(settings);
        ReadPcUi.ViewConfirmation(result);
        return result.Success ? 0 : -1;
    }

}