using Spectre.Console.Cli;
using gm_codex.Application.Services;
using gm_codex.Presentation.ConsoleUI;

namespace gm_codex.Application.Commands;

public class ListNpcCommand : Command
{
    private readonly CharacterService _characterService;
    
    public ListNpcCommand(CharacterService characterService) => _characterService = characterService;

    public override int Execute(CommandContext context)
    { 
        var result = _characterService.ListNonPlayableCharacters();
        ReadPcUi.ViewEntitiesByType(result);
        return 0;
    }
}