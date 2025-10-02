using gm_codex.Application.Common;
using gm_codex.Domain.Models;
using Spectre.Console;

namespace gm_codex.Presentation.ConsoleUI.Import;

public static class ImportUi
{
    public static async Task ViewImportMonstersAsync(Result<int> result)
    {
        if (!result.Success)
        {
            AnsiConsole.MarkupLine($"[red]ERROR[/]: {result.Error}");
        }

        
        await AnsiConsole.Progress()
            .AutoRefresh(false)
            .AutoClear(false)
            .HideCompleted(false)
            .Columns(new ProgressColumn[]
            {
                new TaskDescriptionColumn(),    // Task description
                new ProgressBarColumn(),        // Progress bar
                new PercentageColumn(),         // Percentage
                new RemainingTimeColumn(),      // Remaining time
                new SpinnerColumn(),            // Spinner
                //new DownloadedColumn(),         // Downloaded
                //new TransferSpeedColumn(),      // Transfer speed
            })
            .Start(async ctx =>
            {
                var task1 = ctx.AddTask("[green]Importing monsters...[/]");
                var task2 = ctx.AddTask("[blue]Saving monsters...[/]");

                while (!ctx.IsFinished)
                {
                    //Simulate work
                    await Task.Delay(10);
                    
                    task1.Increment(15);
                    task2.Increment(50);
                }
            });
    }
}