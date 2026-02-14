using gm_codex.Application.Commands.Settings.Party;
using gm_codex.Application.Services;
using gm_codex.Presentation.ConsoleUI.Party;
using Spectre.Console;
using Spectre.Console.Cli;

namespace gm_codex.Application.Commands.Party;

public class ReadPartyCommand : Command<ReadPartySettings>
{
    private readonly PartyService _partyService;

    public ReadPartyCommand(PartyService partyService) => _partyService = partyService;

    public override int Execute(CommandContext context, ReadPartySettings settings)
    {
        if (string.IsNullOrWhiteSpace(settings.Party))
        {
            AnsiConsole.MarkupLine("[red]ERROR:[/]: Name is required.");
            return -1;
        }

        var result = _partyService.GetPartyWithMembers(settings.Party);
        ReadPartyUi.ViewSingleParty(result);
        return result.Success ? 0 : 1;
    }
}

public static class ReadPartyCommandExtensions
{
    public static void AddReadPartyCommand(this IConfigurator<CommandSettings> configuration)
    {
        configuration
            .AddCommand<ReadPartyCommand>("party")
            .WithDescription("Get info on one party");
    }
}

