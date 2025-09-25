using gm_codex.Application.Common;
using gm_codex.Infrastructure.Data;
using Spectre.Console;

namespace gm_codex.Presentation.ConsoleUI.Encounters;

public class ReadEncounterUi
{
    public static void ViewEncounter(Result<EncounterRecord> result)
    {
        if (!result.Success)
        {
            AnsiConsole.MarkupLine($"[red]ERROR[/]: {result.Error}");
            return;
        }
        
        var encounter = result.Value;
        var table = new Table().RoundedBorder();
        
        AnsiConsole.Write(
            new Panel(string.IsNullOrEmpty(encounter?.Description) ? "No Description" : encounter.Description)
                .Header($"[bold yellow]{encounter?.Name}[/]")
                .Border(BoxBorder.Rounded)
                .BorderStyle(new Style(Color.Grey))
        );
        
        table
            .BorderColor(Color.Grey)
            .AddColumn("[yellow]Name[/]")
            .AddColumn("[green]Description[/]")
            .AddColumn("[red]HP[/]")
            .AddColumn("[blue]Initiative[/]")
            .AddColumn("[purple]Conditions[/]");

        if (encounter?.Participants.Count == 0)
        {
            table.AddRow("-", "-", "-", "-", "-");
        }
        else
        {
            int index = 1;
            foreach (var p in encounter!.Participants)
            {
                table.AddRow(
                    index.ToString(),
                    p.DisplayName ?? "-",
                    p.CurrentHp.ToString(),
                    p.Initiative.ToString(),
                    p.Conditions ?? "-"
                );
                index++;
            }
        }
        
        AnsiConsole.Write(table);
    }

    public static void ViewAllEncounters(Result<List<EncounterRecord>> result)
    {
        if (!result.Success)
        {
            AnsiConsole.MarkupLine($"[red]ERROR[/]: {result.Error}");
            return;
        }
        
        var encounters = result.Value;
        var table = new Table().RoundedBorder().Title("[yellow]Encounters[/]");
        
        table
            .AddColumn("Name")
            .AddColumn("Description");

        foreach (var encounter in encounters!)
        {
            table.AddRow(
                encounter.Name,
                encounter.Description ?? "-");
        }
        
        AnsiConsole.Write(table);
    }
}