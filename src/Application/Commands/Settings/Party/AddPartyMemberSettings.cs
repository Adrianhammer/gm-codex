using System.ComponentModel;
using JetBrains.Annotations;
using Spectre.Console.Cli;

namespace gm_codex.Application.Commands.Settings.Party;

public class AddPartyMemberSettings : CommandSettings
{
    [CommandOption("-n|--name <name>")]
    [Description("The name of the party to be created")]
    [UsedImplicitly]
    public required string Name { get; set; }
}