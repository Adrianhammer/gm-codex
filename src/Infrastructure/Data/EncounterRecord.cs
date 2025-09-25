namespace gm_codex.Infrastructure.Data;

public class EncounterRecord
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public List<EncounterParticipantRecord> Participants { get; set; } = new();
}