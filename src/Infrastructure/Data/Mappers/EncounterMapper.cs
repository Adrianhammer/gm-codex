using gm_codex.Domain.Models;

namespace gm_codex.Infrastructure.Data.Mappers;

public static class EncounterMapper
{
    public static Encounter ToDomain(EncounterRecord encounter)
    {
        return new Encounter
        {
            Id = encounter.Id,
            Name = encounter.Name,
            Description = encounter.Description,
        };
    }
}