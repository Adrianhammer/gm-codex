using System.ComponentModel;
using gm_codex.Domain.Enums;
using JetBrains.Annotations;
using Spectre.Console.Cli;

namespace gm_codex.Application.Commands.Settings.Encounters;

public class UpdateEncounterParticipantSetting : CommandSettings
{
    [CommandOption("-n|--entity <name>")]
    [Description("The name of the character to be updated")]
    [UsedImplicitly]
    public required string EntityName { get; set; }

    [CommandOption("-t|--type <type>")]
    [Description("The type of the entity to be updated")]
    [UsedImplicitly]
    public EntityType EntityType { get; set; }

    [CommandOption("-h|--health <health>")]
    [Description("The health of the entity to be updated to")]
    [UsedImplicitly]
    public int? Health { get; set; }

    [CommandOption("-c|--conditions <conditions>")]
    [Description("The conditions of the entity to be updated to")]
    [UsedImplicitly]
    public string? Conditions { get; set; }

    [CommandOption("-e|--encounter <name>")]
    [Description("The name of the encounter to be created")]
    [UsedImplicitly]
    public required string EncounterName { get; set; }
}
