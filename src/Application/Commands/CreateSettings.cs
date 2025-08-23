using System.ComponentModel;
using gm_codex.Domain.Enums;
using Spectre.Console.Cli;

namespace gm_codex.Application.Commands;

public class CreateSettings : CommandSettings
{
    [CommandArgument(0, "<name>")]
    [Description("The name of the entity to be created")]
    public required string Name { get; set; }
    
    [CommandArgument(1, "<type>")]
    [Description("The type of the entity to be created")]
    public required EntityType EntityType { get; set; }
    
    [CommandArgument(2, "<race>")]
    [Description("The race of the entity to be created")]
    public required Race Race { get; set; }
    
    [CommandArgument(3, "<subrace>")]
    [Description("The subrace of the entity to be created")]
    public string? SubRace { get; set; }
    
    [CommandArgument(4, "<class>")]
    [Description("The class of the entity to be created")]
    public required Class EntityClass { get; set; }
    
    [CommandArgument(5, "<subclass>")]
    [Description("The subclass of the entity to be created")]
    public string? SubClass { get; set; }
    
}