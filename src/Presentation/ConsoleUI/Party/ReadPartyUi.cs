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

    public static void ViewSingleParty(Result<Domain.Models.Party> result)
    {
        if (!result.Success)
        {
            AnsiConsole.MarkupLine($"[red]ERROR[/]: {result.Error}");
        }
        
        var rule = new Rule("Party").LeftJustified();
        rule.Style = Style.Parse("white dim");
        AnsiConsole.Write(rule);

        var partyTable = new Table()
            .Border(TableBorder.Rounded)
            .AddColumn("[grey]ID[/]")
            .AddColumn("[yellow]Name[/]")
            .AddColumn("Description");
        
        partyTable.AddRow(
            result.Value!.Id.ToString(), 
            result.Value!.Name, 
            string.IsNullOrWhiteSpace(result.Value!.Description) ? "-" : result.Value!.Description);

        var memberTable = new Table()
            .Border(TableBorder.Rounded)
            .AddColumn("[grey]Id[/]")
            .AddColumn("[yellow]Members[/]")
            .AddColumn("Race")
            .AddColumn("Sub Race")
            .AddColumn("Class")
            .AddColumn("Sub Class");
            

        foreach (var member in result.Value!.Members)
        {
            memberTable.AddRow(
                member.Id.ToString(), 
                member.Name,
                member.Race,
                member.SubRace ?? "-",
                member.EntityClass,
                member.SubClass ?? "-"
                );
        }
        
        AnsiConsole.Write(new Panel(partyTable)
            .Header($"{result.Value.Name}")
            .Border(BoxBorder.None)
            .Expand());
        
        AnsiConsole.Write(new Panel(memberTable)
            .Header("Members")
            .Border(BoxBorder.None)
        .Expand());
        
    }
}