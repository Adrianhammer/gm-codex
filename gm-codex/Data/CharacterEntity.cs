namespace gm_codex.Data;

public class CharacterEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Race { get; set; }
    public string? SubRace { get; set; }
    public required string CharacterClass { get; set; }
    public string? SubClass { get; set; }
}