using System.ComponentModel;
using gm_codex.Domain.Enums;
using JetBrains.Annotations;
using Spectre.Console.Cli;

namespace gm_codex.Application.Commands.Settings.Entities;

public class CreateEntitySettings : CommandSettings
{
    [CommandOption("-n|--name <name>")]
    [Description("The name of the entity to be created")]
    [UsedImplicitly]
    public required string Name { get; set; }

    [CommandOption("-t|--type <entity>")]
    [Description("The type of the entity to be created")]
    public required EntityType EntityType { get; set; }
    
    [CommandOption("-r|--race <race>")]
    [Description("The race of the entity to be created")]
    [UsedImplicitly]
    public required Race Race { get; set; }
    
    [CommandOption("--subrace <subrace>")]
    [Description("The subrace of the entity to be created")]
    [UsedImplicitly]
    public string? SubRace { get; set; }
    
    [CommandOption("-c|--class <class>")]
    [Description("The class of the entity to be created")]
    [UsedImplicitly]
    public required Class EntityClass { get; set; }
    
    [CommandOption("--subclass <class>")]
    [Description("The subclass of the entity to be created")]
    [UsedImplicitly]
    public string? SubClass { get; set; }
    
    [CommandOption("-h|--health <health>")]
    [Description("The health of the entity to be created")]
    [UsedImplicitly]
    public string? MaxHp { get; set; }
    
    [CommandOption("-a|--armor <armor>")]
    [Description("The armor of the entity to be created")]
    [UsedImplicitly]
    public string? ArmorClass  { get; set; }
    
}