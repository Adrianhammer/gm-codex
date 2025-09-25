using gm_codex.Domain.Models;
using gm_codex.Infrastructure.Data;

namespace gm_codex.Infrastructure.Repositories.Interface;

public interface IPartyMemberRepository
{
    int InsertMember(Entity entityId, Party partyId);
    int RemoveMember(Entity entityId, Party partyId);
    PartyMemberRecord GetMemberByPartyId(int partyId);
}