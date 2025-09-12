using gm_codex.Application.Common;
using gm_codex.Domain.Enums;
using gm_codex.Domain.Models;
using gm_codex.Infrastructure.Data;
using gm_codex.Infrastructure.Data.Mappers;
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
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        
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
            AnsiConsole.MarkupLine(
                rows == 1
                    ? "[green]Success[/] Entity created :check_mark_button:"
                    : "[yellow]Warning[/] Insert did not affect anny rows. :warning:"
            );
        }
        catch (Exception e)
        {
            AnsiConsole.MarkupLine($"[red]ERROR[/]: Failed to insert entity to database: {e.Message}");
            throw;
        }
    }

    public void UpdateEntity(string name, EntityType? entityType, Race? race, string? subRace, Class? characterClass, string? subClass, string? maxHp, string? armorClass)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        
        try
        {
            var existingEntity = _repository.GetEntityByName(name);
            
            if (existingEntity is not null)
            {
                var entity = EntityMapper.ToDomain(existingEntity);
                if (race is not null) entity.Race = race.Value;
                if (subRace is not null) entity.SubRace = subRace;
                if (characterClass is not null) entity.EntityClass = characterClass.Value;
                if (subClass is not null) entity.SubClass = subClass;
                if (maxHp is not null) entity.MaxHp = maxHp;
                if (armorClass is not null) entity.ArmorClass = armorClass;
                
                try
                {
                    var rows = _repository.UpdateEntity(entity);
                    AnsiConsole.MarkupLine(
                        rows == 1
                            ? "[green]Success[/] Entity Updated :check_mark_button:"
                            : "[yellow]Warning[/] Update did not affect anny rows. :warning:"
                    );
                }
                catch (Exception e)
                {
                    AnsiConsole.MarkupLine($"[red]ERROR[/]: Failed to update entity: {e.Message}");
                    throw;
                }
            }
            else
            {
                AnsiConsole.MarkupLine($"[yellow]Warning[/] Entity not found: {name}");
            }
        }
        catch (Exception e)
        {
            AnsiConsole.MarkupLine($"[red]ERROR[/] Failed to map entity: {e.Message}");
            throw;
        }
    }

    public Result<EntityRecord> ReadEntity(string name)
    {
        try
        {
            var rows = _repository.GetEntityByName(name);

            if (rows is not null)
            {
                return Result<EntityRecord>.Ok(rows); 
            }
            return Result<EntityRecord>.Fail("Entity not found");
        }
        catch (Exception e)
        {
            return Result<EntityRecord>.Fail($"Failed to get entity: {e.Message}");
        }
    }

    public Result<int> DeleteEntity(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        try
        {
            var existingEntity = _repository.GetEntityByName(name);

            if (existingEntity is not null)
            {
                var rows = _repository.DeleteEntityByName(name);

                if (rows == 1)
                {
                    return Result<int>.Ok(rows);
                }
            }
            return Result<int>.Fail("Could not find entity with the name:");
        }
        catch (Exception e)
        { 
            return Result<int>.Fail($"Failed to delete entity: {e.Message}");
        }
    }
    

    public Result<List<EntityRecord>> ListPlayableCharacters()
    {
        try
        {
            var rows = _repository.GetAllPlayableCharacters().ToList();
            
            if (rows.Count > 0)
            {
                return Result<List<EntityRecord>>.Ok(rows!);
            }
            return Result<List<EntityRecord>>.Fail("No playable characters found");
        }
        catch (Exception e)
        {
            return Result<List<EntityRecord>>.Fail($"Failed to get playable characters: {e.Message}");
        }
    }

    public Result<List<EntityRecord>> ListNonPlayableCharacters()
    {
        try
        {
            var rows = _repository.GetAllNonPlayableCharacters().ToList();

            if (rows.Count > 0)
            {
                return Result<List<EntityRecord>>.Ok(rows!);
            }
            return Result<List<EntityRecord>>.Fail("No npc`s found");

        }
        catch (Exception e)
        {
            return Result<List<EntityRecord>>.Fail($"Failed to get non-playable characters: {e.Message}");
        }
        
    }
}