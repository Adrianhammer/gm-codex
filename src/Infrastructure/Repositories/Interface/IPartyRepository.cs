using gm_codex.Domain.Models;
using gm_codex.Infrastructure.Data;

namespace gm_codex.Infrastructure.Repositories.Interface;

public interface IPartyRepository
{
    int InsertParty(Party party);
    PartyRecord? GetParty(String name);
    IEnumerable<PartyRecord> GetParties();
}