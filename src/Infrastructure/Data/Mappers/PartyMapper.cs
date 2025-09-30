using gm_codex.Application.Commands.Settings.Party;
using gm_codex.Domain.Models;

namespace gm_codex.Infrastructure.Data.Mappers;

public class PartyMapper
{
    public static Party ToDomain(CreatePartySettings settings)
    {
        return new Party
        {
            Name = settings.Name.ToLower(),
            Description = settings.Description?.ToLower(),
        };
    }

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