namespace gm_codex.Infrastructure.Data;

public class EncounterParticipantRecord
{
    public int Id { get; set; }
    public int EncounterId { get; set; }
    public int EntityId { get; set; }
    public required string DisplayName { get; set; }
    public int CurrentHp { get; set; }
    public int Initiative { get; set; }
    public string? Conditions { get; set; }
}