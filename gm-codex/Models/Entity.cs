namespace gm_codex.Models;

public class Entity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string EntityType { get; set; }
    public required Race Race { get; set; }
    public string? SubRace { get; set; }
    public required Class EntityClass { get; set; }
    public string? SubClass { get; set; }
    public string? MaxHp { get; set; }
    public string? ArmorClass { get; set; }
}