using gm_codex.Application.Common;
using gm_codex.Infrastructure.Data;
using Spectre.Console;

namespace gm_codex.Presentation.ConsoleUI;

public static class ReadPcUi
{

    public static void ViewSingleEntity(Result<EntityRecord> result)
    {
        if (!result.Success)
        {
            AnsiConsole.MarkupLine($"[red]ERROR[/]: {result.Error}");
            return;
        }

        var entity = result.Value!;
        var table = new Table().RoundedBorder();
        
        table
            .AddColumn("Name")
            .AddColumn("Entity type")
            .AddColumn("Race")
            .AddColumn("Sub Race")
            .AddColumn("Class")
            .AddColumn("Sub Class")
            .AddColumn("Max HP")
            .AddColumn("Armor Class");

        table.AddRow(
            entity.Name,
            entity.EntityType,
            entity.Race,
            entity.SubRace ?? "-",
            entity.EntityClass,
            entity.SubClass ?? "-",
            entity.MaxHp ?? "-",
            entity.ArmorClass ?? "-"
        );
        AnsiConsole.Write(table);
    }

    public static void ViewPlayableCharacters(Result<List<EntityRecord>> result)
    {
        if (!result.Success)
        {
            AnsiConsole.MarkupLine($"[red]ERROR[/]: {result.Error}");
            return;
        }
        
        var table = new Table().RoundedBorder();

        table
            .AddColumn("Name")
            .AddColumn("Entity type")
            .AddColumn("Race")
            .AddColumn("Sub Race")
            .AddColumn("Class")
            .AddColumn("Sub Class");

        foreach (var character in result.Value!)
        {
            table.AddRow(
                character.Name,
                character.EntityType,
                character.Race,
                character.SubRace ?? "-",
                character.EntityClass,
                character.SubClass ?? "-"
            );
        }
        AnsiConsole.Write(table);
    }
    
    public static void ViewNonPlayableCharacters(IEnumerable<EntityRecord> nonPlayableCharacters)
    {
        var table = new Table();
        table.Border(TableBorder.Rounded);
        
        table.AddColumn(new TableColumn("Name"));
        table.AddColumn(new TableColumn("Entity type"));
        table.AddColumn(new TableColumn("Race"));
        table.AddColumn(new TableColumn("Max HP"));
        table.AddColumn(new TableColumn("Armor Class"));

        foreach (var character in nonPlayableCharacters)
        {
            table.AddRow(
                character.Name,
                character.EntityType,
                character.Race,
                character.MaxHp ?? "-",
                character.ArmorClass ?? "-"
            );
        }
        AnsiConsole.Write(table);
    }
}