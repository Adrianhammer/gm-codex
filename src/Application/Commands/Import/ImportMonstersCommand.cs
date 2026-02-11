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
        await ImportUi.ViewImportMonstersAsync(_importService);
        return 0;
    }
}

public static class ImportMonstersCommandExtensions
{
    public static void AddImportMonstersCommand(this IConfigurator<CommandSettings> configuration)
    {
        configuration
            .AddCommand<ImportMonstersCommand>("monsters")
            .WithDescription("Import monsters from Open5e");
    }
}

