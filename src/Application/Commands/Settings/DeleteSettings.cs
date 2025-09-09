using System.ComponentModel;
using Spectre.Console.Cli;

namespace gm_codex.Application.Commands.Settings;

public class DeleteSettings : CommandSettings
{
    [CommandArgument(0, "<name>")]
    [Description("The name of the entity to delete")]
    public required string Name { get; set; }
}