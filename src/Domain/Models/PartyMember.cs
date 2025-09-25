namespace gm_codex.Domain.Models;

public class PartyMember
{
    public int Id { get; set; }
    public required Party PartyId { get; set; }
    public required Entity EntityId { get; set; }
}