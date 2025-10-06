using gm_codex.Domain.Models;
using gm_codex.Infrastructure.Integrations.Monsters.DTO;

namespace gm_codex.Infrastructure.Interface;

public interface IMonsterDataProvider
{
    Task<IEnumerable<Entity>> GetAllMonstersAsync();
}