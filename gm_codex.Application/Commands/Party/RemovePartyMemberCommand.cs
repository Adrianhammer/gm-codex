using gm_codex.Application.Commands.Settings.Party;
using gm_codex.Application.Services;
using gm_codex.Application.ConsoleUI.Entities;
using Spectre.Console;
using Spectre.Console.Cli;

namespace gm_codex.Application.Commands.Party;

public class RemovePartyMemberCommand : Command<RemovePartyMemberSettings>
{
    private readonly PartyService _partyService;

    public RemovePartyMemberCommand(PartyService partyService) => _partyService = partyService;

    public override int Execute(CommandContext context, RemovePartyMemberSettings settings)
    {
        if (string.IsNullOrWhiteSpace(settings.Name) && string.IsNullOrWhiteSpace(settings.Party))
        {
            AnsiConsole.MarkupLine("[red]ERROR:[/]: Name is required.");
            return -1;
        }

        var result = _partyService.RemoveMemberFromParty(settings.Name, settings.Party);
        ReadPcUi.ViewConfirmation(result);
        return result.Success ? 0 : -1;
    }
}

public static class RemovePartyMemberCommandExtensions
{
    public static void AddRemovePartyMemberCommand(
        this IConfigurator<CommandSettings> configuration
    )
    {
        configuration
            .AddCommand<RemovePartyMemberCommand>("remove")
            .WithDescription("Remove party member");
    }
}

