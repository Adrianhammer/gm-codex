namespace gm_codex.Infrastructure.Integrations.Monsters.DTO;

public class MonsterPageDto
{
    public int Count { get; set; }
    public string? Next { get; set; }
    public string? Previous { get; set; }
    public List<MonsterDTO>? Results { get; set; }
}