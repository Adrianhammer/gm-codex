using gm_codex.Domain.Models;
using gm_codex.Infrastructure.Data;

namespace gm_codex.Infrastructure.Repositories.Interface;

public interface IEncounterRepository
{
    void CreateTable();
    int InsertEncounter(Encounter encounter);
    EncounterRecord? GetEncounter(string name);
}