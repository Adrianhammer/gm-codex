using System.IO;
using Spectre.Console;
namespace gm_codex.Application.Services.ConsoleUI;

public class ConsoleUi
{

    public void RenderStartScreen()
    {
        var rule = new Rule("[red]TTRPG Console[/]");
        rule.Centered();
        AnsiConsole.Write(rule);

        var font = FigletFont.Load(Path.Combine("Resources/Fonts", "Delta Corps Priest 1.flf"));
            
        AnsiConsole.Write(
            new FigletText(font, "TTRPG Console")
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

        table.AddRow("help", "Get a list of available commands");
        table.AddRow("create-character", "Create a character");
        table.AddRow("read-character", "Read a character");
        table.AddRow("exit", "Exits the application");
        
        table.Border(TableBorder.Horizontal);
        table.Centered();
        
        AnsiConsole.Write(table);
        
    }
    
}