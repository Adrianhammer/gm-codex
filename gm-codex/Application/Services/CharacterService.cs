using System;
using gm_codex.ConsoleUI;
using gm_codex.Data;
using gm_codex.Models;
using Spectre.Console;

namespace gm_codex.Services;

public class CharacterService
{
    private readonly EntityRepository _repository;

    public CharacterService(EntityRepository repository)
    {
        _repository = repository;
    }
    
    public void CreateEntity()
    {
        var characterData = CreatePcUi.CreateCharacterCommand();
        if (characterData == null)
        {
            AnsiConsole.MarkupLine("[red]ERROR[/]: Entity creation aborted due to invalid input.");
            return;
        }

        var (name, race, entityType, subRace, characterClass, subClass) = characterData.Value;
        
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
}