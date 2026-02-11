using gm_codex.Application.Commands.Settings.Entities;
using gm_codex.Application.Services;
using gm_codex.Domain.Enums;
using gm_codex.Presentation.ConsoleUI.Entities;
using Spectre.Console;
using Spectre.Console.Cli;

namespace gm_codex.Application.Commands.Entities;

public class DeleteNpcCommand : Command<DeleteEntitySettings>
{
    private readonly EntityService _entityService;

    public DeleteNpcCommand(EntityService entityService) => _entityService = entityService;

    public override int Execute(CommandContext context, DeleteEntitySettings entitySettings)
    {
        if (string.IsNullOrWhiteSpace(entitySettings.Name))
        {
            AnsiConsole.MarkupLine("[red]ERROR:[/] Name is required.");
            return -1;
        }

        entitySettings.EntityType = context.Name == "npc" ? EntityType.npc : EntityType.pc;

        var result = _entityService.DeleteEntity(entitySettings.Name, entitySettings.EntityType);
        DeleteEntityUi.ViewDeleteEntity(result, entitySettings.Name);
        return result.Success ? 0 : -1;
    }
}

public static class DeleteNpcCommandExtensions
{
    public static void AddDeleteNpcCommand(this IConfigurator<CommandSettings> configuration)
    {
        configuration
            .AddCommand<DeleteNpcCommand>("npc")
            .WithDescription("Delete one non-playable character");
    }
}

