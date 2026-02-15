using gm_codex.Application.Common;
using gm_codex.Application.Services;
using gm_codex.Authorization;
using Spectre.Console;

namespace gm_codex.Presentation.ConsoleUI.Music;

public class PlayMusicUi
{
    public static void ViewPlaylistAsync(SpotifyResponseModels.SpotifyUserProfileResponse response )
    {
        AnsiConsole.Markup("Profile:");
        AnsiConsole.Markup($"[green]Name:[/] {response.DisplayName}");
        AnsiConsole.Markup($"[green]Country:[/]{response.Country}");
    }
}