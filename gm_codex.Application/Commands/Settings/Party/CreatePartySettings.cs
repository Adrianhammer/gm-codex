using System.ComponentModel;
using JetBrains.Annotations;
using Spectre.Console.Cli;

namespace gm_codex.Application.Commands.Settings.Party;

public class CreatePartySettings : CommandSettings
{
    [CommandOption("-n|--name <name>")]
    [Description("The name of the party to be created")]
    [UsedImplicitly]
    public required string Name { get; set; }
    
    [CommandOption("-d|--description <name>")]
    [Description("The description of the party to be created")]
    [UsedImplicitly]
    public string? Description { get; set; }
    
}