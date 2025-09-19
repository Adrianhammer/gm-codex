namespace gm_codex.Domain.Models;

public class Encounter
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
}