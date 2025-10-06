namespace gm_codex.Infrastructure.Integrations.Monsters.DTO;

public class MonsterPageDto
{
    public int count { get; set; }
    public string? next { get; set; }
    public string? previous { get; set; }
    public List<MonsterDto>? results { get; set; }
}