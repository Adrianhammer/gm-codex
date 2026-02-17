using gm_codex.Application.Services;
using gm_codex.Application.ConsoleUI;
using Spectre.Console.Cli;

namespace gm_codex.Application.Commands;

public class ListAllCommand : Command
{
    private readonly EncounterService _encounterService;
    private readonly EntityService _entityService;

    public ListAllCommand(EncounterService encounterService, EntityService entityService)
    {
        _encounterService = encounterService;
        _entityService = entityService;
    }

    public override int Execute(CommandContext context)
    {
        var encounterResult = _encounterService.ListAllEncounters();
        var npcResult = _entityService.ListNonPlayableCharacters();
        var pcResult = _entityService.ListPlayableCharacters();
        ListAllUi.ViewAll(encounterResult, npcResult, pcResult);
        return 0;
    }
}

public static class ListAllCommandExtensions
{
    public static void AddListAllCommand(this IConfigurator<CommandSettings> configuration)
    {
        configuration.AddCommand<ListAllCommand>("all").WithDescription("List everything");
    }
}

