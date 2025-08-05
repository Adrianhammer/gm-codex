using System.ComponentModel.DataAnnotations;

namespace gm_codex.Models;

public class Character
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required Race Race { get; set; }
    public string? SubRace { get; set; }
    public required Class CharacterClass { get; set; }
    public string? SubClass { get; set; }
}