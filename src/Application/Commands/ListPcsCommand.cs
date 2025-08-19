using Spectre.Console.Cli;
using gm_codex.Application.Services;
namespace gm_codex.Application.Commands;

public class ListPcsCommand : Command
{
    //private readonly CharacterService _characterService;
    public static CharacterService? CharacterService { get; set; }
/*
    public ListPcsCommand(CharacterService characterService)
    {
        _characterService = characterService;
    }
*/
    public override int Execute(CommandContext context)
    {
        CharacterService?.ListPlayableCharacters();
        return 0;
    }
}