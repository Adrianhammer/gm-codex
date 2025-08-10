using gm_codex.Domain.Enums;
using gm_codex.Infrastructure.Data;
using gm_codex.Infrastructure.Repositories;
using Spectre.Console;

namespace gm_codex.Views.ConsoleUI;

public static class ReadPcUi
{

    public static void ViewSingleEntity(EntityRecord entity)
    {
        var table = new Table();
        table.Border(TableBorder.Rounded);
        
        table.AddColumn(new TableColumn("Name"));
        table.AddColumn(new TableColumn("Entity type"));
        table.AddColumn(new TableColumn("Race"));
        table.AddColumn(new TableColumn("Sub Race"));
        table.AddColumn(new TableColumn("Class"));
        table.AddColumn(new TableColumn("Sub Class"));
        table.AddColumn(new TableColumn("Max HP"));
        table.AddColumn(new TableColumn("Armor Class"));

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

    public static void ViewPlayableCharacters(IEnumerable<EntityRecord> playableCharacters)
    {
        var table = new Table();
        table.Border(TableBorder.Rounded);
        
        table.AddColumn(new TableColumn("Name"));
        table.AddColumn(new TableColumn("Entity type"));
        table.AddColumn(new TableColumn("Race"));
        table.AddColumn(new TableColumn("Sub Race"));
        table.AddColumn(new TableColumn("Class"));
        table.AddColumn(new TableColumn("Sub Class"));

        foreach (var character in playableCharacters)
        {
            table.AddRow(
                character.Name,
                character.EntityType,
                character.Race,
                character.SubRace ?? "-",
                character.SubClass ?? "-"
            );
        }
        AnsiConsole.Write(table);
    }
}