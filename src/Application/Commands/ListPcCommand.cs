using Spectre.Console.Cli;
using gm_codex.Application.Services;
namespace gm_codex.Application.Commands;

public class ListPcCommand : Command
{
    private readonly CharacterService _characterService;
    public ListPcCommand(CharacterService characterService) => _characterService = characterService;
    public override int Execute(CommandContext context)
    {
        _characterService.ListPlayableCharacters();
        return 0;
    }
}