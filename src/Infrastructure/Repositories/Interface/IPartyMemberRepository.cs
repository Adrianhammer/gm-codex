using gm_codex.Infrastructure.Data;

namespace gm_codex.Infrastructure.Repositories.Interface;

public interface IPartyMemberRepository
{
    int InsertMember(int partyId, int entityId);
    int RemoveMember(int partyId, int entityId);
    PartyMemberRecord GetMemberByPartyId(int partyId);
}