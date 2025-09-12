using Spectre.Console.Cli;
using gm_codex.Application.Services;
using gm_codex.Presentation.ConsoleUI;

namespace gm_codex.Application.Commands;

public class ListPcCommand : Command
{
    private readonly CharacterService _characterService;
    public ListPcCommand(CharacterService characterService) => _characterService = characterService;
    public override int Execute(CommandContext context)
    {
        var result = _characterService.ListPlayableCharacters();
        ReadPcUi.ViewEntitiesByType(result);
        
        return result.Success ? 0 : -1;
    }
}