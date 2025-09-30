using gm_codex.Application.Services;
using Spectre.Console.Cli;

namespace gm_codex.Application.Commands.Import;

public class ImportMonstersCommand : Command
{
    private readonly ImportService _importService;
    
    public ImportMonstersCommand(ImportService importService) => _importService = importService;

    public override int Execute(CommandContext context)
    {
        var result = _importService.ImportAllMonsters();
        return result.Success ? 0 : 1;
    }
}