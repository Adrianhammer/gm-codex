using gm_codex.Application.Common;
using gm_codex.Domain.Enums;
using gm_codex.Domain.Models;
using gm_codex.Infrastructure.Data;
using gm_codex.Infrastructure.Data.Mappers;
using gm_codex.Infrastructure.Repositories.Interface;

namespace gm_codex.Application.Services;

public class EncounterParticipantService
{
    private readonly IEncounterParticipantRepository _participantRepository;
    private readonly IEncounterRepository _encounterRepository;
    private readonly IEntityRepository _entityRepository;

    public EncounterParticipantService(
        IEncounterParticipantRepository participantRepository,
        IEncounterRepository encounterRepository,
        IEntityRepository entityRepository
    )
    {
        _participantRepository = participantRepository;
        _encounterRepository = encounterRepository;
        _entityRepository = entityRepository;
    }

    public Result<int> AddParticipantsToEncounter(
        string encounterName,
        IEnumerable<string> npc,
        IEnumerable<string> pc
    )
    {
        ArgumentNullException.ThrowIfNull(encounterName);
        ArgumentNullException.ThrowIfNull(npc);

        try
        {
            var existingEncounter = EnsureEncounterExists(encounterName);
            if (existingEncounter is null)
            {
                return Result<int>.Fail($"Encounter '{encounterName}' does not exist.");
            }

            int insertedCount = 0;

            foreach (var spec in npc)
            {
                var (entityName, count) = ParseNpcSpec(spec);

                var entity = EnsureEntityExists(entityName);
                if (entity is null)
                {
                    return Result<int>.Fail($"Entity '{entityName}' not found.");
                }
                var domainEntity = EntityMapper.ToDomain(entity);

                // How many of this entity already in the encounter
                int existingCount = _participantRepository.GetParticipantCount(
                    existingEncounter.Id,
                    domainEntity.Id
                );

                for (int i = 0; i < count; i++)
                {
                    var participant = BuildParticipant(
                        existingEncounter.Id,
                        domainEntity,
                        existingCount + i + 1
                    );
                    insertedCount += _participantRepository.InsertParticipant(participant);
                }
            }
            foreach (var spec in pc)
            {
                var (entityName, count) = ParsePcSpec(spec);
                var entity = EnsureEntityExists(entityName, EntityType.pc);
                if (entity is null)
                {
                    return Result<int>.Fail($"Entity '{entityName}' not found.");
                }
                var domainEntity = EntityMapper.ToDomain(entity);
                int existingCount = _participantRepository.GetParticipantCount(
                    existingEncounter.Id,
                    domainEntity.Id
                );
                if (existingCount >= 1)
                {
                    continue;
                }
                for (int i = 0; i < count; i++)
                {
                    var participant = BuildParticipant(existingEncounter.Id, domainEntity);
                    insertedCount += _participantRepository.InsertParticipant(participant);
                }
            }

            return insertedCount > 0
                ? Result<int>.Ok(insertedCount)
                : Result<int>.Fail("No participants were added.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Result<int>.Fail(e.Message);
        }
    }

    private EncounterRecord? EnsureEncounterExists(string encounterName) =>
        _encounterRepository.GetEncounter(encounterName);

    private EntityRecord? EnsureEntityExists(
        string entityName,
        EntityType entityType = EntityType.npc
    ) => _entityRepository.GetEntityByName(entityName, entityType);

    private (string name, int count) ParseNpcSpec(string spec)
    {
        var parts = spec.Split(":", StringSplitOptions.TrimEntries);
        var name = parts[0];
        int count = parts.Length > 1 && int.TryParse(parts[1], out var n) ? n : 1;
        return (name, count);
    }

    private (string name, int count) ParsePcSpec(string spec)
    {
        return (spec, 1);
    }

    private EncounterParticipant BuildParticipant(int encounterId, Entity entity)
    {
        Random random = new Random();
        return new EncounterParticipant
        {
            EncounterId = encounterId,
            EntityId = entity.Id,
            Name = $"{entity.Name}",
            Initiative = random.Next(1, 21),
            CurrentHp = entity.MaxHp is not null ? int.Parse(entity.MaxHp!) : 0,
        };
    }

    private EncounterParticipant BuildParticipant(int encounterId, Entity entity, int index)
    {
        Random random = new Random();
        return new EncounterParticipant
        {
            EncounterId = encounterId,
            EntityId = entity.Id,
            Name = $"{entity.Name} #{index}",
            Initiative = random.Next(1, 21),
            CurrentHp = entity.MaxHp is not null ? int.Parse(entity.MaxHp!) : 0,
        };
    }
}
