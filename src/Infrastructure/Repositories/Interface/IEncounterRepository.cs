using gm_codex.Domain.Models;
using gm_codex.Infrastructure.Data;

namespace gm_codex.Infrastructure.Repositories.Interface;

public interface IEncounterRepository
{
    void CreateTable();
    int InsertEncounter(Encounter encounter);
    int UpdateEncounter(Encounter encounter);
    EncounterRecord? GetEncounter(string name);
    IEnumerable<EncounterRecord> GetAllEncounters();
    int DeleteEncounter(string name);
}