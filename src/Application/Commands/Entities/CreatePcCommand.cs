using gm_codex.Application.Commands.Settings.Entities;
using gm_codex.Application.Services;
using gm_codex.Domain.Enums;
using gm_codex.Presentation.ConsoleUI.Entities;
using Spectre.Console;
using Spectre.Console.Cli;

namespace gm_codex.Application.Commands.Entities;

public class CreatePcCommand : Command<CreateEntitySettings>
{
    private readonly EntityService _entityService;

    public CreatePcCommand(EntityService entityService) => _entityService = entityService;

    public override int Execute(CommandContext context, CreateEntitySettings entitySettings)
    {
        if (string.IsNullOrWhiteSpace(entitySettings.Name))
        {
            AnsiConsole.MarkupLine("[red]ERROR:[/]: Name is required.");
            return -1;
        }

        entitySettings.EntityType = context.Name == "pc" ? EntityType.pc : EntityType.npc;

        var result = _entityService.CreateEntity(
            entitySettings.Name,
            entitySettings.EntityType,
            entitySettings.Race,
            entitySettings.SubRace,
            entitySettings.EntityClass,
            entitySettings.SubClass,
            entitySettings.MaxHp,
            entitySettings.ArmorClass
        );
        ReadPcUi.ViewConfirmation(result);
        return result.Success ? 0 : -1;
    }
}

public static class CreatePcCommandExtensions
{
    public static void AddCreatePcCommand(this IConfigurator<CommandSettings> configuration)
    {
        configuration
            .AddCommand<CreatePcCommand>("pc")
            .WithDescription("Create one playable character");
    }
}

