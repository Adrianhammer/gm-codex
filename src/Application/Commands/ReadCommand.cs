using gm_codex.Application.Commands.Settings;
using gm_codex.Application.Services;
using gm_codex.Presentation.ConsoleUI;
using Spectre.Console;
using Spectre.Console.Cli;

namespace gm_codex.Application.Commands;

public class ReadCommand : Command<ReadSettings>
{
    private readonly CharacterService _characterService;

    public ReadCommand(CharacterService characterService) => _characterService = characterService;

    public override int Execute(CommandContext context, ReadSettings settings)
    {
        if (string.IsNullOrWhiteSpace(settings.Name))
        {
            AnsiConsole.MarkupLine("[red]ERROR:[/]: Name is required.");
            return -1;
        }
        
        var result = _characterService.ReadEntity(settings.Name, settings.EntityType );
        ReadPcUi.ViewSingleEntity(result);
        
        return result.Success ? 0 : -1;
    }
}