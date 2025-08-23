using System.ComponentModel;
using gm_codex.Domain.Enums;
using Spectre.Console.Cli;

namespace gm_codex.Application.Commands;

public class CreateSettings : CommandSettings
{
    [CommandOption("-n|--name <name>")]
    [Description("The name of the entity to be created")]
    public required string Name { get; set; }
    
    [CommandOption("-t|--type <entity>")]
    [Description("The type of the entity to be created")]
    public required EntityType EntityType { get; set; }
    
    [CommandOption("-r|--race <race>")]
    [Description("The race of the entity to be created")]
    public required Race Race { get; set; }
    
    [CommandOption("--subrace <subrace>")]
    [Description("The subrace of the entity to be created")]
    public string? SubRace { get; set; }
    
    [CommandOption("-c|--class <class>")]
    [Description("The class of the entity to be created")]
    public required Class EntityClass { get; set; }
    
    [CommandOption("--subclass <class>")]
    [Description("The subclass of the entity to be created")]
    public string? SubClass { get; set; }
    
}