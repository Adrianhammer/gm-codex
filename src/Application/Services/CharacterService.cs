using gm_codex.Domain.Enums;
using gm_codex.Domain.Models;
using gm_codex.Infrastructure.Repositories;
using gm_codex.Infrastructure.Repositories.Interface;
using gm_codex.Presentation.ConsoleUI;
using Spectre.Console;

namespace gm_codex.Application.Services;

public class CharacterService
{
    private readonly IEntityRepository _repository;
    public CharacterService(IEntityRepository repository) => _repository = repository;
    
    
    public void CreateEntity(string name, EntityType entityType, Race race, string? subRace, Class characterClass, string? subClass, string? maxHp, string? armorClass)
    {
        
        var character = new Entity
        {
            Name = name.ToLower(),
            Race = race,
            EntityType = entityType,
            SubRace = subRace?.ToLower(),
            EntityClass = characterClass,
            SubClass = subClass?.ToLower(),
            MaxHp = maxHp,
            ArmorClass = armorClass
        };

        try
        {
            var rows = _repository.InsertEntity(character);
            
            if (rows == 1)
            {
                AnsiConsole.MarkupLine("[green]Success[/] Entity created :check_mark_button:");       
            }
            else
            {
                AnsiConsole.MarkupLine("[yellow]Warning[/] Insert did not affect anny rows. :warning:[/]");
            }
        }
        catch (Exception e)
        {
            AnsiConsole.MarkupLine($"[red]ERROR[/]: Failed to insert entity to database: {e.Message}");
            throw;
        }
    }

    public void ReadEntity(string name)
    {
        var entity = _repository.GetEntityByName(name);

        if (entity is null)
        {
            AnsiConsole.MarkupLineInterpolated($"[red]ERROR[/]: Entity '{name}' not found");
            return;
        }
        
        ReadPcUi.ViewSingleEntity(entity);
    }

    public void DeleteEntity(string name)
    {
        var entity = name;

        if (string.IsNullOrWhiteSpace(entity))
        {
            AnsiConsole.MarkupLine("[red]ERROR[/]: Entity deletion aborted due to invalid input.");
            return;
        }

        _repository.DeleteEntityByName(entity);
        
        DeleteEntityUi.ViewDeleteEntity(entity);
        
    }

    public void ListPlayableCharacters()
    {
        var playableCharacters = _repository.GetAllPlayableCharacters().ToList();

        if (!playableCharacters.Any())
        {
            AnsiConsole.MarkupLine("[yellow]INFO[/]: No playable characters found");
            return;
        }
        
        ReadPcUi.ViewPlayableCharacters(playableCharacters);
    }

    public void ListNonPlayableCharacters()
    {
        var nonPlayableCharacters = _repository.GetAllNonPlayableCharacters().ToList();

        if (!nonPlayableCharacters.Any())
        {
            AnsiConsole.MarkupLine("[yellow]INFO[/]: No NPC´s found");
            return;
        }
        ReadPcUi.ViewNonPlayableCharacters(nonPlayableCharacters);
    }
}