using System.Diagnostics;
using gm_codex.Application.Services;
using Spectre.Console;

namespace gm_codex.Presentation.ConsoleUI.Import;

public static class ImportUi
{
    public static async Task ViewImportMonstersAsync(ImportService service)
    {
        string[] flavourText =
        new [] {
            "Summoning creatures from the abyss...", 
            "Summoning creatures from the abyss...", 
            "Checking for mimics... oh wait, all of them are mimics.",
            "Polishing troll clubs...",
            "Sneaking kobolds into the dungeon...",
            "Rolling monster HP (average, don’t worry)...",
            "Releasing the kraken... cautiously.",
            "Counting how many tentacles are too many...",
            "Soon done!",
            "Rolling dice behind the screen (honestly, trust me)...",
            "Checking the Monster Manual index... A for Aboleth, Z for Zombie...",
            "Converting CR to TPK probability...",
            "Feeding the mimic… careful with your hand.",
            "Balancing goblins on each other’s shoulders for a trenchcoat disguise...",
            "Teaching owlbears how to hoo and growl in sync...",
            "Convincing the lich to smile for the portrait...",
            "Polishing gelatinous cubes until crystal clear...",
            "Waking up the rust monsters (lock away the swords!)",
            "Telling beholders their eye‑rays are all equally beautiful.",
            "Warning: Dragons may contain traces of treasure."
        };
        

        await AnsiConsole.Status()
            .Spinner(Spinner.Known.Dots)
            .SpinnerStyle(Style.Parse("green"))
            .StartAsync("text", async ctx =>
            {
                
                var rand = new Random();
                var stopWatch = Stopwatch.StartNew();

                var shuffled = flavourText.OrderBy(x => rand.Next()).ToArray();

                var importTask = service.ImportAllMonstersAsync();

                while (!importTask.IsCompleted)
                {
                    foreach (var phrase in shuffled)
                    {
                        ctx.Status = phrase;
                        await Task.WhenAny(importTask, Task.Delay(5000));
                    }
                }
                
                var result = await importTask;
                stopWatch.Stop();
                TimeSpan elapsed = stopWatch.Elapsed;
                

                if (!result.Success)
                {
                    AnsiConsole.MarkupLine($"[red]ERROR[/]: {result.Error}");
                }
                else
                {
                    var elapsedMsg = elapsed.Minutes > 0
                        ? $"{elapsed.Minutes}m {elapsed.Seconds}s"
                        : $"{elapsed.Seconds}s";
                    AnsiConsole.MarkupLine($":check_mark_button: [green]{result.Value} monsters imported![/] Total time elapsed: [yellow]{elapsedMsg}[/]");
                }
            });
    }
}