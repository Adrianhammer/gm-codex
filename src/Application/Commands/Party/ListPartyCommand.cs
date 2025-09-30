using gm_codex.Application.Commands.Settings.Party;
using gm_codex.Application.Services;
using gm_codex.Presentation.ConsoleUI.Party;
using Spectre.Console;
using Spectre.Console.Cli;

namespace gm_codex.Application.Commands.Party;

public class ListPartyCommand : Command
{
    private readonly PartyService _service;
    
    public ListPartyCommand(PartyService service) => _service = service;

    public override int Execute(CommandContext context)
    {
        var result = _service.ListAllParties();
        ReadPartyUi.ViewAllParties(result);
        return result.Success ? 0 : -1;
    }
}