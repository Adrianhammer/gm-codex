using gm_codex.Domain.Enums;
using gm_codex.Domain.Models;
using gm_codex.Infrastructure.Data;

namespace gm_codex.Infrastructure.Repositories.Interface;

public interface IEntityRepository
{
    int InsertEntity(Entity entity);
    int UpdateEntity(Entity entity);
    EntityRecord? GetEntityByName(string name, EntityType entityType);
    IEnumerable<EntityRecord>? GetEntitiesById(IEnumerable<int> id);
    int DeleteEntityByName(string name,  EntityType entityType);
    IEnumerable<EntityRecord> GetAllPlayableCharacters();
    IEnumerable<EntityRecord> GetAllNonPlayableCharacters();
    int ImportEntities();
}