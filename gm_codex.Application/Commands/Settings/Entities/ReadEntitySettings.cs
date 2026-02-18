using System.ComponentModel;
using gm_codex.Domain.Enums;
using JetBrains.Annotations;
using Spectre.Console.Cli;

namespace gm_codex.Application.Commands.Settings.Entities;

public class ReadEntitySettings : CommandSettings
{
    [CommandArgument(0, "<name>")]
    [Description("The name of the entity to read")]
    [UsedImplicitly]
    public required string Name { get; set; }
    
    [CommandOption("-t|--type <entity>")]
    [Description("The type of the entity to be created")]
    public required EntityType EntityType { get; set; }
}