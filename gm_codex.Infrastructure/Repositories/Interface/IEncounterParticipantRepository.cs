using gm_codex.Domain.Models;
using gm_codex.Infrastructure.Data;

namespace gm_codex.Infrastructure.Repositories.Interface;

public interface IEncounterParticipantRepository
{
    EncounterParticipantRecord? GetEncounterIdByName(string encounterName);
    EncounterParticipantRecord? GetEncounterParticipantById(int encounterId, int entityId);
    IEnumerable<EncounterParticipantRecord> GetEncounterById(int encounterId);
    int UpdateParticipant(EncounterParticipantRecord participant);
    int GetParticipantCount(int encounterId, int entityId);
    int InsertParticipant(EncounterParticipant participant);
}
