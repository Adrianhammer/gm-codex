using System.IO;
using Spectre.Console;
namespace gm_codex.Presentation.ConsoleUI;

public class ConsoleUi
{
    public void RenderStartScreen()
    {
        var rule = new Rule("[red]GM Codex[/]");
    }
    
    public static void RenderHelpScreen()
    {
        var rule = new Rule("[red]TTRPG Console[/]");
        rule.Centered();
        AnsiConsole.Write(rule);

        var font = FigletFont.Load(Path.Combine("Resources/Fonts", "Delta Corps Priest 1.flf"));
            
        AnsiConsole.Write(
            new FigletText(font, "GM Codex")
                .Centered()
                .Color(Color.IndianRed)
        );
        AnsiConsole.WriteLine();
        
        AnsiConsole.Write(new Align(
            new Markup("[bold]Design under construction[/] " + Emoji.Known.HammerAndWrench + "\nWill add more and better commands later"),
            HorizontalAlignment.Center,
            VerticalAlignment.Bottom)
        );
        
        var table = new Table();

        table.AddColumn("Commands");
        table.AddColumn(new TableColumn("Description").Centered());

        table.AddRow("[yellow]gmctl help[/]", "Show this help screen");
        table.AddRow("[yellow]gmctl list pcs[/]", "List all playable characters");
        table.AddRow("[yellow]gmctl delete[/]", "delete entity");
        table.AddRow("[yellow]gmctl create pc[/]", "create entity");
        table.AddRow("[yellow]gmctl --help[/]", "Show built-in command help");
        
        table.Border(TableBorder.Horizontal);
        table.Centered();
        
        AnsiConsole.Write(table);
    }
}