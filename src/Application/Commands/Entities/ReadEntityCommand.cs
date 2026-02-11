using gm_codex.Application.Commands.Settings.Entities;
using gm_codex.Application.Services;
using gm_codex.Presentation.ConsoleUI.Entities;
using Spectre.Console;
using Spectre.Console.Cli;

namespace gm_codex.Application.Commands.Entities;

public class ReadEntityCommand : Command<ReadEntitySettings>
{
    private readonly EntityService _entityService;

    public ReadEntityCommand(EntityService entityService) => _entityService = entityService;

    public override int Execute(CommandContext context, ReadEntitySettings entitySettings)
    {
        if (string.IsNullOrWhiteSpace(entitySettings.Name))
        {
            AnsiConsole.MarkupLine("[red]ERROR:[/]: Name is required.");
            return -1;
        }

        var result = _entityService.ReadEntity(entitySettings.Name, entitySettings.EntityType);
        ReadPcUi.ViewSingleEntity(result);
        return result.Success ? 0 : -1;
    }
}

public static class ReadEntityCommandExtensions
{
    public static void AddReadEntityCommand(this IConfigurator<CommandSettings> configuration)
    {
        configuration.AddCommand<ReadEntityCommand>("entity").WithDescription("Read one entity");
    }
}

