using gm_codex.Application.Commands.Settings.Entities;
using gm_codex.Application.Services;
using gm_codex.Domain.Enums;
using gm_codex.Presentation.ConsoleUI.Entities;
using Spectre.Console;
using Spectre.Console.Cli;

namespace gm_codex.Application.Commands.Entities;

public class DeleteNpcCommand : Command<DeleteEntitySettings>
{
    private readonly CharacterService _characterService;

    public DeleteNpcCommand(CharacterService characterService) => _characterService = characterService;

    public override int Execute(CommandContext context, DeleteEntitySettings entitySettings)
    {
        if (string.IsNullOrWhiteSpace(entitySettings.Name))
        {
            AnsiConsole.MarkupLine("[red]ERROR:[/] Name is required.");
            return -1;
        }
        
        entitySettings.EntityType = context.Name == "npc" ? EntityType.npc : EntityType.pc;
        
        var result = _characterService.DeleteEntity(entitySettings.Name, entitySettings.EntityType);
        DeleteEntityUi.ViewDeleteEntity(result, entitySettings.Name);
        return result.Success ? 0 : -1;
    }
}