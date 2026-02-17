namespace gm_codex.Domain.Models;

public class EncounterParticipant
{
    public int Id { get; set; }
    public int EncounterId { get; set; }
    public int EntityId { get; set; }
    public required string Name { get; set; }
    public int CurrentHp { get; set; }
    public int Initiative { get; set; }
    public string? Conditions { get; set; }
}