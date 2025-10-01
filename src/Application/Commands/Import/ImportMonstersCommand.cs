using gm_codex.Application.Services;
using gm_codex.Presentation.ConsoleUI.Import;
using Spectre.Console.Cli;

namespace gm_codex.Application.Commands.Import;

public class ImportMonstersCommand : AsyncCommand
{
    private readonly ImportService _importService;
    public ImportMonstersCommand(ImportService importService) => _importService = importService;

    public override async Task<int> ExecuteAsync(CommandContext context)
    {
        var result = _importService.ImportAllMonsters();
        await ImportUi.ViewImportMonstersAsync(result);
        return result.Success ? 0 : 1;
    }
}