using gm_codex.Domain.Models;

namespace gm_codex.Infrastructure.Data;

public class PartyRecord
{
    public int Id { get; set; }
    public required String Name { get; set; }
    public String? Description { get; set; }
    
    public List<Entity> Members  { get; set; } = new List<Entity>();
}