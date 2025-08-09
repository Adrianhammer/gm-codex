using System;
using gm_codex.Domain.Models;
using gm_codex.Infrastructure.Repositories;
using gm_codex.Views.ConsoleUI;
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

    public void ReadEntity()
    {
        var name = ReadPcUi.ReadCharacterCommand();
        
        if (string.IsNullOrWhiteSpace(name))
        {
            AnsiConsole.MarkupLine("[red]ERROR[/]: Entity creation aborted due to invalid input.");
            return;
        }

        var entity = _repository.ReadEntity(name);

        if (entity == null)
        {
            AnsiConsole.MarkupLine("[red]ERROR[/]: Entity not found");
            return;
        }
        ReadPcUi.RenderEntityRead(entity);
    }
}