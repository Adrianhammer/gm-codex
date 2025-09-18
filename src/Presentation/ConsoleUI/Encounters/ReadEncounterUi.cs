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

        table
            .AddColumn("Name")
            .AddColumn("Description");

        table.AddRow(
                encounter!.Name,
                encounter.Description ?? "-"
            );
        
        AnsiConsole.Write(table);
    }
}