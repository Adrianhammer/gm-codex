using gm_codex.Domain.Enums;
using gm_codex.Infrastructure.Data;
using gm_codex.Infrastructure.Repositories;
using Spectre.Console;

namespace gm_codex.Views.ConsoleUI;

public static class ReadPcUi
{

    public static string? ReadCharacterCommand()
    {
        AnsiConsole.MarkupLine("Type in [bold]Name[/] to retrieve an entity [italic](not case sensitive, but needs to be accurate)[/]");
        var nameInput = Console.ReadLine();
        
        if (string.IsNullOrEmpty(nameInput))
        {
            Console.WriteLine("Name is empty");
        }
        
        return nameInput;
    }

    public static void RenderEntityRead(EntityRecord entity)
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
}