using System.ComponentModel;
using JetBrains.Annotations;
using Spectre.Console.Cli;

namespace gm_codex.Application.Commands.Settings.Encounters;

public class EncounterParticipantSetting : CommandSettings
{
    [CommandOption("-n|--npc <NPC>")]
    [Description("Add one or more NPC types with optional counts, e.g. goblin:3 troll:2")]
    [UsedImplicitly]
    public required string[] Npc { get; set; } = Array.Empty<string>();

    [CommandOption("-p|--pc <PC>")]
    [Description("Add one or more PC types with optional counts, e.g. albert john")]
    [UsedImplicitly]
    public required string[] Pc { get; set; } = Array.Empty<string>();

    [CommandOption("-e|--encounter <name>")]
    [Description("The name of the encounter to be created")]
    [UsedImplicitly]
    public required string EncounterName { get; set; }
}

