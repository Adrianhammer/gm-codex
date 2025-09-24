using gm_codex.Application.Common;
using gm_codex.Domain.Models;
using gm_codex.Infrastructure.Data;
using gm_codex.Infrastructure.Data.Mappers;
using gm_codex.Infrastructure.Repositories.Interface;

namespace gm_codex.Application.Services;

public class EncounterService
{
    private readonly IEncounterRepository _repository;
    private readonly IEncounterParticipantRepository _participantRepository;

    public EncounterService(IEncounterRepository repository, IEncounterParticipantRepository participantRepository)
    {
        _repository = repository;
        _participantRepository = participantRepository;
    } 
    
    public Result<int> CreateEncounter(string name, string? description)
    {
        ArgumentNullException.ThrowIfNull(name);
        
        try
        {
            var existingEncounter = _repository.GetEncounter(name);

            if (existingEncounter is not null)
            {
                return Result<int>.Fail($"Encounter '{name}' already exists");
            }
            
            var encounter = new Encounter
            {
                Name = name,
                Description = description,
            };

            var rows = _repository.InsertEncounter(encounter);
            
            if (rows == 1)
            {
                return Result<int>.Ok(rows);
            }
            return Result<int>.Fail($"Failed to insert encounter '{name}'");
        }
        catch (Exception e)
        {
            return Result<int>.Fail($"Create encounter failed: {e.Message}");
        }
    }

    public Result<int> UpdateEncounter(string name, string? description)
    {
        ArgumentNullException.ThrowIfNull(name);

        try
        {
            var existingEncounter = _repository.GetEncounter(name);
            
            if (existingEncounter is null)
            {
                return Result<int>.Fail($"Encounter '{name}' does not exist");
            }
            
            var encounter = EncounterMapper.ToDomain(existingEncounter);
            if(name != encounter.Name) encounter.Name = name;
            if (description != encounter.Description) encounter.Description = description;

            try
            {
                var rows = _repository.UpdateEncounter(encounter);
                return rows == 1 ? Result<int>.Ok(rows) : Result<int>.Fail($"Failed to update encounter '{name}'"); 
            }
            catch (Exception e)
            {
                return Result<int>.Fail($"Failed to update encounter '{name}': {e.Message}");
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Result<EncounterRecord> ReadEncounter(string name)
    {
        ArgumentNullException.ThrowIfNull(name);

        try
        {
            var encounter = _repository.GetEncounter(name);
            if (encounter is null)
            {
                return Result<EncounterRecord>.Fail($"Encounter '{name}' not found");
            }

            var participants = _participantRepository.GetEncounterById(encounter.Id);
            encounter.Participants = participants.ToList();
            
            return Result<EncounterRecord>.Ok(encounter);
        }
        catch (Exception e)
        {
            return Result<EncounterRecord>.Fail($"Read encounter failed: {e.Message}");
        }
    }

    public Result<List<EncounterRecord>> ListAllEncounters()
    {
        try
        {
            var encounters = _repository.GetAllEncounters().ToList();
            if (encounters.Count > 0)
            {
                return Result<List<EncounterRecord>>.Ok(encounters);
            }
            return Result<List<EncounterRecord>>.Fail("Encounters not found");
        }
        catch (Exception e)
        {
            return Result<List<EncounterRecord>>.Fail($"List encounters failed: {e.Message}");
        }
    }

    public Result<int> DeleteEncounter(string name)
    {
        ArgumentNullException.ThrowIfNull(name);

        try
        {
            var existingEncounter = _repository.GetEncounter(name);
            
            if (existingEncounter is not null)
            {
                var rows = _repository.DeleteEncounter(name);
                if (rows == 1)
                {
                    return Result<int>.Ok(rows);
                }
            }
            return Result<int>.Fail($"Encounter '{name}' not found");
            
        }
        catch (Exception e)
        {
            return Result<int>.Fail($"Delete encounter failed: {e.Message}");
        }
    }
}