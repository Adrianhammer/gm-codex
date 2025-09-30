using gm_codex.Application.Commands.Settings.Party;
using gm_codex.Application.Common;
using gm_codex.Domain.Enums;
using gm_codex.Domain.Models;
using gm_codex.Infrastructure.Data;
using gm_codex.Infrastructure.Data.Mappers;
using gm_codex.Infrastructure.Repositories.Interface;

namespace gm_codex.Application.Services;

public class PartyService
{
    private readonly IPartyRepository  _partyRepository;
    private readonly IPartyMemberRepository _partyMemberRepository;
    private readonly IEntityRepository _entityRepository;

    public PartyService(IPartyRepository partyRepository, IPartyMemberRepository partyMemberRepository,IEntityRepository entityRepository)
    {
        _partyRepository = partyRepository;
        _partyMemberRepository = partyMemberRepository;
        _entityRepository = entityRepository;
    } 

    public Result<int> CreateParty(CreatePartySettings settings)
    {
        ArgumentNullException.ThrowIfNull(settings);
        
        try
        {
            var party = PartyMapper.ToDomain(settings);

            var existingParty = _partyRepository.GetParty(party.Name);
            if (existingParty is not null)
            {
                return Result<int>.Fail($"Party with name '{party.Name}' already exists.");
            }
            
            var rows = _partyRepository.InsertParty(party);

            return rows != 1 
                ? Result<int>.Fail("Something went wrong.") 
                : Result<int>.Ok(rows);

        }
        catch (Exception e)
        {
            return Result<int>.Fail(e.Message);
        }
    }

    public Result<Party> GetPartyWithMembers(string partyName)
    {
        ArgumentNullException.ThrowIfNull(partyName);

        try
        {
            var party = _partyRepository.GetParty(partyName);
            if (party is null)
            {
                return Result<Party>.Fail($"Party with name '{partyName}' does not exist.");
            }
            
            var members = _partyMemberRepository.GetMemberByPartyId(party.Id);
            if (members is null)
            {
                return Result<Party>.Fail($"Party '{partyName}' has no members.");
            }
            
            var entityIds = members.Select(m => m.EntityId);
            var entities = _entityRepository.GetEntitiesById(entityIds);
            if (entities is null) return Result<Party>.Fail($"Party '{partyName}' has no members.");
            
            
            var domainParty = new Party
            {
                Id = party.Id,
                Name = party.Name,
                Description = party.Description,
                Members = entities.ToList()
            };

            return Result<Party>.Ok(domainParty);

        }
        catch (Exception e)
        {
            return Result<Party>.Fail(e.Message);
        }
    }

    public Result<List<PartyRecord>> ListAllParties()
    {
        try
        {
            var rows = _partyRepository.GetParties().ToList();

        return rows.Count == 0
                ? Result<List<PartyRecord>>.Fail("No parties found.")
                : Result<List<PartyRecord>>.Ok(rows);
        
        }
        catch (Exception e)
        {
            return Result<List<PartyRecord>>.Fail($"Something went wrong. {e.Message}");
        }
    }

    public Result<int> AddEntityToParty(string partyName, string entityName)
    {
        ArgumentNullException.ThrowIfNull(partyName);
        ArgumentNullException.ThrowIfNull(entityName);


        try
        {
            var existingParty = _partyRepository.GetParty(partyName);
            if (existingParty is null)
            {
                return Result<int>.Fail($"Party with name '{partyName}' does not exist.");
            }


            var existingPc = _entityRepository.GetEntityByName(entityName, EntityType.pc);
            
            if (existingPc is null)
            {
                return Result<int>.Fail($"Character with name '{existingPc}' does not exist.");
            }

            var rows = _partyMemberRepository.InsertMember(existingParty.Id, existingPc.Id);

            return rows != 1
                ? Result<int>.Fail("Something went wrong.")
                : Result<int>.Ok(rows);

        }
        catch (Exception e)
        {
            return Result<int>.Fail(e.Message);
        }
    }
    
    public Result<int> RemoveMemberFromParty(string partyName, string entityName)
    {
        ArgumentNullException.ThrowIfNull(partyName);
        ArgumentNullException.ThrowIfNull(entityName);

        try
        {
            var existingParty = _partyRepository.GetParty(partyName);
            if (existingParty is null)
            {
                return Result<int>.Fail($"Party with name '{partyName}' does not exist.");
            }

            var existingPc = _entityRepository.GetEntityByName(entityName, EntityType.pc);
            
            if (existingPc is null)
            {
                return Result<int>.Fail($"Character with name '{entityName}' does not exist.");
            }
            
            var rows = _partyMemberRepository.RemoveMember(existingParty.Id, existingPc.Id);
            return rows != 1
                ? Result<int>.Fail("Something went wrong.")
                : Result<int>.Ok(rows);
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}