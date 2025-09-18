using gm_codex.Application.Common;
using gm_codex.Domain.Models;
using gm_codex.Infrastructure.Data;
using gm_codex.Infrastructure.Repositories.Interface;

namespace gm_codex.Application.Services;

public class EncounterService
{
    private readonly IEncounterRepository _repository;
    
    public EncounterService(IEncounterRepository repository) => _repository = repository;
    
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
}