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
        var url = "v1/monsters/";
        
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
                    MaxHp = dto.MaxHp.ToString(),
                    ArmorClass = dto.ArmorClass.ToString()
                });
            }
        }
        return monsters;
    }
}