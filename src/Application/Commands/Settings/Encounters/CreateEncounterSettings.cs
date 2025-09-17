using System.ComponentModel;
using JetBrains.Annotations;
using Spectre.Console.Cli;

namespace gm_codex.Application.Commands.Settings.Encounters;

public class CreateEncounterSettings : CommandSettings
{
    [CommandOption("-n|--name <name>")]
    [Description("The name of the encounter to be created")]
    [UsedImplicitly]
    public required string Name { get; set; }
    
    [CommandOption("-d|--description <description>")]
    [Description("The description of the encounter to be created")]
    [UsedImplicitly]
    public string? Description { get; set; }
}