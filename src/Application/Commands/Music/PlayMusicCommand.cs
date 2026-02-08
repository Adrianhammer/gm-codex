using gm_codex.Application.Commands.Settings.Music;
using gm_codex.Application.Services;
using gm_codex.Presentation.ConsoleUI.Music;
using Spectre.Console;
using Spectre.Console.Cli;

namespace gm_codex.Application.Commands.Music;

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
        
        var result = _musicService.PlayMusicAsync();
        await PlayMusicUi.ViewPlaylistAsync(_musicService);
        return 0;
    }
}