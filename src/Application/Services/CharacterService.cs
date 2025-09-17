using gm_codex.Application.Common;
using gm_codex.Domain.Enums;
using gm_codex.Domain.Models;
using gm_codex.Infrastructure.Data;
using gm_codex.Infrastructure.Data.Mappers;
using gm_codex.Infrastructure.Repositories.Interface;

namespace gm_codex.Application.Services;

public class CharacterService
{
    private readonly IEntityRepository _repository;
    public CharacterService(IEntityRepository repository) => _repository = repository;
    
    public Result<int> CreateEntity(string name, EntityType entityType, Race race, string? subRace, Class characterClass, string? subClass, string? maxHp, string? armorClass)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        try
        {
            var existingEntity = _repository.GetEntityByName(name, entityType);

            if (existingEntity is not null)
            {
                return Result<int>.Fail($"Entity '{name}' already exists");
            }
            
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
                    return Result<int>.Ok(rows);
                }
                return Result<int>.Fail("Failed to insert character");
            }
            
            catch (Exception e)
            {
                return Result<int>.Fail(e.Message);
            }
            
        }
        catch (Exception e)
        {
            return Result<int>.Fail(e.Message);
        }
        
        

        
    }

    public Result<int> UpdateEntity(string name, EntityType entityType, Race? race, string? subRace, Class? characterClass, string? subClass, string? maxHp, string? armorClass)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        
        try
        {
            var existingEntity = _repository.GetEntityByName(name, entityType);

            if (existingEntity is null)
            {
                return Result<int>.Fail("Entity not found");
            }

            var entity = EntityMapper.ToDomain(existingEntity);
                entity.EntityType = entityType;
                if (race is not null) entity.Race = race.Value;
                if (subRace is not null) entity.SubRace = subRace;
                if (characterClass is not null) entity.EntityClass = characterClass.Value;
                if (subClass is not null) entity.SubClass = subClass;
                if (maxHp is not null) entity.MaxHp = maxHp;
                if (armorClass is not null) entity.ArmorClass = armorClass;
                
                try
                {
                    var rows = _repository.UpdateEntity(entity);

                    return rows == 1 ? Result<int>.Ok(rows) : Result<int>.Fail("Failed to update character");

                }
                catch (Exception e)
                {
                    return Result<int>.Fail($"WHAT " + e.Message);
                }
        }
        catch (Exception e)
        {
            return Result<int>.Fail($"HERE " + e.Message);
        }
    }

    public Result<EntityRecord> ReadEntity(string name, EntityType? entityType)
    {
        try
        {
            var rows = _repository.GetEntityByName(name, entityType!.Value);

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

    public Result<int> DeleteEntity(string name,  EntityType? entityType)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        try
        {
            var existingEntity = _repository.GetEntityByName(name, entityType!.Value);

            if (existingEntity is not null)
            {
                var rows = _repository.DeleteEntityByName(existingEntity.Name, entityType!.Value);

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