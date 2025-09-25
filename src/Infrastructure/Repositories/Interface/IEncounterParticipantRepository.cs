using gm_codex.Domain.Models;
using gm_codex.Infrastructure.Data;

namespace gm_codex.Infrastructure.Repositories.Interface;

public interface IEncounterParticipantRepository
{
    EncounterParticipantRecord? GetEncounterIdByName (string encounterName);
    IEnumerable<EncounterParticipantRecord> GetEncounterById (int encounterId);
    int GetParticipantCount(int encounterId, int entityId);
    int InsertParticipant(EncounterParticipant participant);
}