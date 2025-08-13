using System.IO;
using Spectre.Console;
namespace gm_codex.Presentation.ConsoleUI;

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
        table.AddRow("read-entity", "Read an entity");
        table.AddRow("delete-entity", "Delete an entity");
        table.AddRow("list-pcs", "List all stored characters");
        table.AddRow("exit", "Exits the application");
        
        table.Border(TableBorder.Horizontal);
        table.Centered();
        
        AnsiConsole.Write(table);
        
    }
    
}