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