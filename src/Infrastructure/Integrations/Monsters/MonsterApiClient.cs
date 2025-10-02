using gm_codex.Domain.Enums;
using gm_codex.Domain.Models;
using gm_codex.Infrastructure.Integrations.Monsters.DTO;
using gm_codex.Infrastructure.Integrations.Open5e;
using gm_codex.Infrastructure.Interface;

namespace gm_codex.Infrastructure.Integrations.Monsters;

public class MonsterApiClient : IMonsterDataProvider
{
    private readonly Open5EApiClient _open5EApiClient;
    
    public MonsterApiClient (Open5EApiClient open5EApiClient) => _open5EApiClient = open5EApiClient;
    
    public async Task<IEnumerable<Entity>> GetAllMonstersAsync()
    {
        
        var monsters = new List<Entity>();
        var url = "monsters/?limit=5";
        
        var page = await _open5EApiClient.GetAsync<MonsterPageDto>(url);

        if (page?.Results != null)
        {
            foreach (var dto in page.Results)
            {
                monsters.Add(new Entity
                {
                    Name = dto.Name,
                    EntityType = EntityType.npc,
                    Race = Race.alseid,
                    EntityClass = Class.monster,
                    MaxHp = dto.hit_points.ToString(),
                    ArmorClass = dto.armor_class.ToString()
                });
            }
        }
        return monsters;
    }
}