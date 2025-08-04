namespace gm_codex.Models;

public class Character
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Race { get; set; }
    public string? SubRace { get; set; }
    public string CharacterClass { get; set; }
    public string? SubClass { get; set; }
}