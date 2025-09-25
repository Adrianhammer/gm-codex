using gm_codex.Infrastructure.Data;

namespace gm_codex.Infrastructure.Repositories.Interface;

public interface IPartyRepository
{
    int InsertParty(String name, String description);
    PartyRecord? GetParty(String name);
    IEnumerable<PartyRecord?> GetParties();
}