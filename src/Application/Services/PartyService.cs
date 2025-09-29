using gm_codex.Application.Commands.Settings.Party;
using gm_codex.Application.Common;
using gm_codex.Infrastructure.Data;
using gm_codex.Infrastructure.Data.Mappers;
using gm_codex.Infrastructure.Repositories.Interface;

namespace gm_codex.Application.Services;

public class PartyService
{
    private readonly IPartyRepository  _repository;
    
    public PartyService(IPartyRepository repository) => _repository = repository;

    public Result<int> CreateParty(CreatePartySettings settings)
    {
        ArgumentNullException.ThrowIfNull(settings);
        
        try
        {
            var party = PartyMapper.ToDomain(settings);

            var existingParty = _repository.GetParty(party.Name);
            if (existingParty is not null)
            {
                return Result<int>.Fail($"Party with name '{party.Name}' already exists.");
            }
            
            var rows = _repository.InsertParty(party);

            return rows != 1 
                ? Result<int>.Fail("Something went wrong.") 
                : Result<int>.Ok(rows);

        }
        catch (Exception e)
        {
            return Result<int>.Fail(e.Message);
        }
    }

    public Result<List<PartyRecord>> ListAllParties()
    {
        try
        {
            var rows = _repository.GetParties().ToList();

        return rows.Count == 0
                ? Result<List<PartyRecord>>.Fail("No parties found.")
                : Result<List<PartyRecord>>.Ok(rows);
        
        }
        catch (Exception e)
        {
            return Result<List<PartyRecord>>.Fail($"Something went wrong. {e.Message}");
        }
    }

    public Result<int> AddPcToParty(AddPartyMemberSettings settings)
    {
        ArgumentNullException.ThrowIfNull(settings);

        try
        {
            var existingParty = _repository.GetParty(settings.Name);
            if (existingParty is null)
            {
                return Result<int>.Fail($"Party with name '{settings.Name}' does not exist.");
            }
            
            // Continue further
            return Result<int>.Ok(1);
        }
        catch (Exception e)
        {
            return Result<int>.Fail(e.Message);
        }
    }
}