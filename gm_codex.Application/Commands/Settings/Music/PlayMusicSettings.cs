using System.ComponentModel;
using JetBrains.Annotations;
using Spectre.Console.Cli;

namespace gm_codex.Application.Commands.Settings.Music;

public class PlayMusicSettings : CommandSettings
{
    [CommandOption("-p|--playlist <playlist>")]
    [Description("Decide which playlist to play")]
    [UsedImplicitly]
    public required string Playlist { get; set; }
}