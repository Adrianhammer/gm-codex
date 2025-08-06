using Spectre.Console;
namespace gm_codex.ConsoleUI;

//Class for general ui of app
public class ConsoleUi
{

    public void RenderStartScreen()
    {
        var rule = new Rule("[red]TTRPG Console[/]");
        rule.Centered();
        AnsiConsole.Write(rule);

        var font = FigletFont.Load(Path.Combine("Fonts", "Delta Corps Priest 1.flf"));
            
        AnsiConsole.Write(
            new FigletText(font, "TTRPG Console")
                .Centered()
                .Color(Color.IndianRed)
            );
        AnsiConsole.WriteLine();
        
        AnsiConsole.Write(new Align(
            new Markup("[bold]Design under construction[/] " + Emoji.Known.HammerAndWrench + "\nWill fix commands later"),
            HorizontalAlignment.Center,
            VerticalAlignment.Bottom)
        );
        
        var table = new Table();

        table.AddColumn("Select options");
        table.AddColumn(new TableColumn("Description").Centered());

        table.AddRow("1", "Create character");
        
        table.Border(TableBorder.Simple);
        table.Centered();
        
        AnsiConsole.Write(table);
        
    }
    
}