using System.ComponentModel;
using JetBrains.Annotations;
using Spectre.Console.Cli;

namespace gm_codex.Application.Commands.Settings.Encounters;

public class ReadEncounterSettings : CommandSettings
{
    [CommandArgument(0, "<name>")]
    [Description("The name of the entity to read")]
    [UsedImplicitly]
    public required string Name { get; set; }
}