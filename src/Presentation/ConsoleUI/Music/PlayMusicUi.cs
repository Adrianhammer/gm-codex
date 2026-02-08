using gm_codex.Application.Common;
using gm_codex.Application.Services;
using Spectre.Console;

namespace gm_codex.Presentation.ConsoleUI.Music;

public class PlayMusicUi
{
    public static async Task ViewPlaylistAsync(MusicService musicService)
    {
        AnsiConsole.Markup("Playing music...");
    }
}