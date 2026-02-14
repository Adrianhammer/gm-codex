using gm_codex.Application.Services;
using gm_codex.Presentation.ConsoleUI.Entities;
using Spectre.Console.Cli;

namespace gm_codex.Application.Commands.Entities;

public class ListNpcCommand : Command
{
    private readonly EntityService _entityService;

    public ListNpcCommand(EntityService entityService) => _entityService = entityService;

    public override int Execute(CommandContext context)
    {
        var result = _entityService.ListNonPlayableCharacters();
        ReadPcUi.ViewEntitiesByType(result);
        return 0;
    }
}

public static class ListNpcCommandExtensions
{
    public static void AddListNpcCommand(this IConfigurator<CommandSettings> configuration)
    {
        configuration
            .AddCommand<ListNpcCommand>("npc")
            .WithDescription("List all non playable characters");
    }
}

