using gm_codex.Application.Services;
using gm_codex.Presentation.ConsoleUI.Entities;
using Spectre.Console.Cli;

namespace gm_codex.Application.Commands.Entities;

public class ListPcCommand : Command
{
    private readonly EntityService _entityService;
    public ListPcCommand(EntityService entityService) => _entityService = entityService;
    public override int Execute(CommandContext context)
    {
        var result = _entityService.ListPlayableCharacters();
        ReadPcUi.ViewEntitiesByType(result);
        
        return result.Success ? 0 : -1;
    }
}