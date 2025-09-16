using gm_codex.Application.Commands.Settings;
using gm_codex.Application.Services;
using gm_codex.Domain.Enums;
using gm_codex.Presentation.ConsoleUI;
using Spectre.Console;
using Spectre.Console.Cli;

namespace gm_codex.Application.Commands;

public class UpdateNpcCommand : Command<UpdateSettings>
{
    private readonly CharacterService _characterService;
    
    public UpdateNpcCommand(CharacterService characterService) => _characterService = characterService;

    public override int Execute(CommandContext context, UpdateSettings settings)
    {
        if (string.IsNullOrWhiteSpace(settings.Name))
        {
            AnsiConsole.MarkupLine("[red]ERROR:[/]: Name is required.");
            return -1;
        }
        
        settings.EntityType = context.Name == "npc" ? EntityType.npc : EntityType.pc;

        var result = _characterService.UpdateEntity(settings.Name, settings.EntityType, settings.Race, settings.SubRace, settings.EntityClass, settings.SubClass, settings.MaxHp, settings.ArmorClass);
        ReadPcUi.ViewConfirmation(result);
        return result.Success ? 0 : -1;
    }
}