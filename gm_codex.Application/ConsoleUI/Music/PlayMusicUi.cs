using gm_codex.Application.Common;
using gm_codex.Application.Services;
using gm_codex.Infrastructure.Integrations.Spotify.Models;
using Spectre.Console;

namespace gm_codex.Presentation.ConsoleUI.Music;

public class PlayMusicUi
{
    public static void ViewPlaylistAsync(SpotifyResponseModels.SpotifyUserProfileResponse response )
    {
        AnsiConsole.Markup("Profile:\n");
        AnsiConsole.Markup($"[green]You have now authorized Spotify to GM Coodex! Spotify username[/]: {response.DisplayName}");
    }
}