using gm_codex.Application.Commands.Settings.Music;
using gm_codex.Application.ConsoleUI.Music;
using gm_codex.Application.Services;
using Spectre.Console;
using Spectre.Console.Cli;

namespace gm_codex.Application.Commands.Music;

// TODO: Await service call and use its result.
// If failed, print error from Result and return non-zero exit code.
// If success, pass result data to UI (do not call UI blindly).

public class PlayMusicCommand : AsyncCommand<PlayMusicSettings>
{
    private readonly MusicService _musicService;

    public PlayMusicCommand(MusicService musicService) => _musicService = musicService;

    public override async Task<int> ExecuteAsync(CommandContext context, PlayMusicSettings settings)
    {
        if (string.IsNullOrWhiteSpace(settings.Playlist))
        {
            AnsiConsole.MarkupLine("[red]ERROR:[/]: Playlist is required.");
            return -1;
        }

        var result = await _musicService.GetProfileAsync();

        if (!result.Success || result.Value == null)
        {
            AnsiConsole.MarkupLine("[red]ERROR:[/]: Music playback failed: " + result.Error);
            return -1;
        }

        PlayMusicUi.ViewPlaylistAsync(result.Value);
        return 0;
    }
}

public static class PlayMusicCommandExtensions
{
    public static void AddPlayMusicCommand(this IConfigurator<CommandSettings> configuration)
    {
        configuration.AddCommand<PlayMusicCommand>("music").WithDescription("Play music");
    }
}
