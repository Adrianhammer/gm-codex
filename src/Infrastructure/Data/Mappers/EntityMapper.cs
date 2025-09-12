namespace gm_codex.Infrastructure.Data.Mappers;
using Domain.Enums;
using Domain.Models;

public static class EntityMapper
{
    public static Entity ToDomain(EntityRecord record)
    {
        return new Entity
        {
            Id = record.Id,
            Name = record.Name,
            EntityType = Enum.Parse<EntityType>(record.EntityType),
            Race = Enum.Parse<Race>(record.Race),
            SubRace = record.SubRace,
            EntityClass = Enum.Parse<Class>(record.EntityClass),
            SubClass = record.SubClass,
            MaxHp = record.MaxHp,
            ArmorClass = record.ArmorClass,
        };
    }
}