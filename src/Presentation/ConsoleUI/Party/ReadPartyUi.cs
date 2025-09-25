using gm_codex.Application.Common;
using gm_codex.Infrastructure.Data;
using Spectre.Console;

namespace gm_codex.Presentation.ConsoleUI.Party;

public class ReadPartyUi
{
    public static void ViewAllParties(Result<List<PartyRecord>> result)
    {
        if (!result.Success)
        {
            AnsiConsole.MarkupLine($"[red]ERROR[/]: {result.Error}");
            return;
        }

        var parties = result.Value!;
        if (!parties.Any())
        {
            AnsiConsole.MarkupLine("[yellow]No parties exist.[/]");
            return;
        }

        // Title
        var rule = new Rule("Parties").LeftJustified();
        rule.Style = Style.Parse("white dim");
        AnsiConsole.Write(rule);

        // Render each party
        foreach (var party in parties)
        {
            var table = new Table()
                .Border(TableBorder.Rounded)
                .AddColumn("[grey]ID[/]")
                .AddColumn("[yellow]Name[/]")
                .AddColumn("Description");

            table.AddRow(
                party.Id.ToString(),
                $"[yellow]{party.Name}[/]",
                string.IsNullOrWhiteSpace(party.Description) ? "-" : party.Description
            );

            AnsiConsole.Write(new Panel(table)
                .Header($"{party.Name}")
                .Border(BoxBorder.None)
                .Expand());

            AnsiConsole.WriteLine();
        }
    }
}