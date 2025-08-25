using Spectre.Console.Cli;
using gm_codex.Application.Services;
namespace gm_codex.Application.Commands;

public class ListNpcCommand : Command
{
    public static CharacterService? CharacterService { get; set; }

    public override int Execute(CommandContext context)
    {
        CharacterService?.ListNonPlayableCharacters();
        return 0;
    }
}