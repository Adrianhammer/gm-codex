using gm_codex.Application.Commands.Settings.Party;
using gm_codex.Application.Services;
using gm_codex.Application.ConsoleUI.Entities;
using Spectre.Console;
using Spectre.Console.Cli;

namespace gm_codex.Application.Commands.Party;

public class CreatePartyCommand : Command<CreatePartySettings>
{
    private readonly PartyService _partyService;

    public CreatePartyCommand(PartyService partyService) => _partyService = partyService;

    public override int Execute(CommandContext context, CreatePartySettings settings)
    {
        if (string.IsNullOrWhiteSpace(settings.Name))
        {
            AnsiConsole.MarkupLine("[red]ERROR:[/]: Name is required.");
            return -1;
        }

        var result = _partyService.CreateParty(settings);
        ReadPcUi.ViewConfirmation(result);
        return result.Success ? 0 : 1;
    }
}

public static class CreatePartyCommandExtensions
{
    public static void AddCreatePartyCommand(this IConfigurator<CommandSettings> configuration)
    {
        configuration.AddCommand<CreatePartyCommand>("party").WithDescription("Create a party");
    }
}

