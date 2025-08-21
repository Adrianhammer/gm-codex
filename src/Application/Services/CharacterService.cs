using System;
using gm_codex.Application.Commands;
using gm_codex.Domain.Models;
using gm_codex.Infrastructure.Repositories;
using gm_codex.Presentation.ConsoleUI;
using Spectre.Console;

namespace gm_codex.Application.Services;

public class CharacterService
{
    private readonly EntityRepository _repository;

    public CharacterService(EntityRepository repository)
    {
        _repository = repository;
    }
    
    public void CreateEntity()
    {
        var entityData = CreatePcUi.CreateCharacterCommand();
        if (entityData == null)
        {
            AnsiConsole.MarkupLine("[red]ERROR[/]: Entity creation aborted due to invalid input.");
            return;
        }

        var (name, race, entityType, subRace, characterClass, subClass) = entityData.Value;
        
        var character = new Entity
        {
            Name = name,
            Race = race,
            EntityType = entityType,
            SubRace = subRace,
            EntityClass = characterClass,
            SubClass = subClass
        };
        
        _repository.InsertEntity(character);
        Console.WriteLine("Entity created");
        AnsiConsole.MarkupLine("[green]Success[/] Entity created :check_mark_button:");
    }

    public void ReadEntity(string name)
    {
        var entity = _repository.GetEntityByName(name);

        if (entity == null)
        {
            AnsiConsole.MarkupLineInterpolated($"[red]ERROR[/]: Entity '{name}' not found");
            return;
        }
        
        ReadPcUi.ViewSingleEntity(entity);
    }

    public void DeleteEntity()
    {
        var name = EntityCommands.DeleteEntityCommand();

        if (string.IsNullOrWhiteSpace(name))
        {
            AnsiConsole.MarkupLine("[red]ERROR[/]: Entity deletion aborted due to invalid input.");
            return;
        }

        _repository.DeleteEntityByName(name);
        
        DeleteEntityUi.ViewDeleteEntity(name);
        
    }

    public void ListPlayableCharacters()
    {
        var playableCharacters = _repository.GetAllPlayableCharacters();

        if (!playableCharacters.Any())
        {
            AnsiConsole.MarkupLine("[yellow]INFO[/]: No playable characters found");
            return;
        }
        
        ReadPcUi.ViewPlayableCharacters(playableCharacters);
    }
}