namespace gm_codex.Domain.Models;

public class Party
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }

    public List<Entity> Members { get; set; } = new();
}
