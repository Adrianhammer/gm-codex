using System.ComponentModel;
using JetBrains.Annotations;
using Spectre.Console.Cli;

namespace gm_codex.Application.Commands.Settings.Party;

public class AddPartyMemberSettings : CommandSettings
{
    [CommandOption("-n|--name <name>")]
    [Description("The name of the character to be added")]
    [UsedImplicitly]
    public required string Name { get; set; }
    
    [CommandOption("-p|--party <party>")]
    [Description("The name of the party")]
    [UsedImplicitly]
    public required string Party { get; set; }
}