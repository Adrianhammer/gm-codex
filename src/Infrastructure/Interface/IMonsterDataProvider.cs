using gm_codex.Domain.Models;

namespace gm_codex.Infrastructure.Interface;

public interface IMonsterDataProvider
{
    Task<IEnumerable<Entity>> GetAllMonstersAsync();
}