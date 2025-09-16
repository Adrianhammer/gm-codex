using gm_codex.Application.Commands.Settings;
using gm_codex.Application.Services;
using gm_codex.Domain.Enums;
using gm_codex.Presentation.ConsoleUI;
using Spectre.Console;
using Spectre.Console.Cli;

namespace gm_codex.Application.Commands;

public class DeletePcCommand : Command<DeleteSettings>
{
    private readonly CharacterService _characterService;

    public DeletePcCommand(CharacterService characterService) => _characterService = characterService;

    public override int Execute(CommandContext context, DeleteSettings settings)
    {
        if (string.IsNullOrWhiteSpace(settings.Name))
        {
            AnsiConsole.MarkupLine("[red]ERROR:[/] Name is required.");
            return -1;
        }
        
        settings.EntityType = context.Name == "pc" ? EntityType.pc : EntityType.npc;
        
        var result = _characterService.DeleteEntity(settings.Name, settings.EntityType);
        DeleteEntityUi.ViewDeleteEntity(result, settings.Name);
        return result.Success ? 0 : -1;
    }
}