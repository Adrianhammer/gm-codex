using gm_codex.Application.Commands.Settings;
using gm_codex.Application.Services;
using gm_codex.Domain.Enums;
using Spectre.Console;
using Spectre.Console.Cli;

namespace gm_codex.Application.Commands;

public class CreateNpcCommand : Command<CreateSettings>
{
    private readonly CharacterService _characterService;

    public CreateNpcCommand(CharacterService characterService) => _characterService = characterService;

    public override int Execute(CommandContext context, CreateSettings settings)
    {
        if (string.IsNullOrWhiteSpace(settings.Name))
        {
            AnsiConsole.MarkupLine("[red]ERROR:[/]: Name is required.");
            return -1;
        }
        
        settings.EntityType = context.Name == "npc" ? EntityType.npc : EntityType.pc;
        
        _characterService.CreateEntity(settings.Name, settings.EntityType, settings.Race, settings.SubRace, settings.EntityClass, settings.SubClass, settings.MaxHp, settings.ArmorClass);
        return 0;
    }
}