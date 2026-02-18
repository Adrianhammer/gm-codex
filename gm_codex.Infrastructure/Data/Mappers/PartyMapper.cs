using gm_codex.Domain.Models;

namespace gm_codex.Infrastructure.Data.Mappers;

public class PartyMapper
{
    public static PartyRecord ToRecord(Party party)
    {
        return new PartyRecord
        {
            Id = party.Id,
            Name = party.Name.ToLower(),
            Description = party.Description?.ToLower(),
        };
    }
}
