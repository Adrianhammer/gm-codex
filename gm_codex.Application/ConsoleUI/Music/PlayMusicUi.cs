using gm_codex.Contracts.Abstractions.Integrations.Spotify.Models;
using Spectre.Console;

namespace gm_codex.Application.ConsoleUI.Music;

public class PlayMusicUi
{
    public static void ViewPlaylistAsync(SpotifyUserProfile response)
    {
        AnsiConsole.Markup("Profile:\n");
        AnsiConsole.Markup(
            $"[green]You have now authorized Spotify to GM Coodex! Spotify username[/]: {response.DisplayName}"
        );
    }
}
