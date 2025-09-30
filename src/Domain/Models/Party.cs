using gm_codex.Infrastructure.Data;

namespace gm_codex.Domain.Models;

public class Party
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    
    public List<EntityRecord> Members  { get; set; } = new();
}