using System.ComponentModel;
using JetBrains.Annotations;
using Spectre.Console.Cli;

namespace gm_codex.Application.Commands.Settings.Party;

public class ReadPartySettings : CommandSettings
{
    [CommandOption("-p|--party <party>")]
    [Description("The name of the party")]
    [UsedImplicitly]
    public required string Party { get; set; }
}