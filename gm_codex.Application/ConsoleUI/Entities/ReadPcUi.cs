using gm_codex.Application.Common;
using gm_codex.Infrastructure.Data;
using Spectre.Console;

namespace gm_codex.Application.ConsoleUI.Entities;

public static class ReadPcUi
{
    public static void ViewConfirmation(Result<int> result)
    {
        if (!result.Success)
        {
            AnsiConsole.MarkupLine($"[red]ERROR[/]: {result.Error}");
            return;
        }
        
        AnsiConsole.MarkupLine("[green]Success[/] Changes Saved! :check_mark_button:");
    }
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

    public static void ViewEntitiesByType(Result<List<EntityRecord>> result)
    {
        if (!result.Success)
        {
            AnsiConsole.MarkupLine($"[red]ERROR[/]: {result.Error}");
            return;
        }

        var table = new Table().RoundedBorder().Title(result.Value!.FirstOrDefault()?.EntityType == "pc" ? "[blue]Playable characters[/]" : "[red]Non-Playable characters[/]");

        table
            .AddColumn("Name")
            .AddColumn("Entity type")
            .AddColumn("Race")
            .AddColumn("Sub Race")
            .AddColumn("Class")
            .AddColumn("Sub Class")
            .AddColumn("Max HP")
            .AddColumn("Armor Class");

        foreach (var character in result.Value!)
        {
            table.AddRow(
                character.Name,
                character.EntityType,
                character.Race,
                character.SubRace ?? "-",
                character.EntityClass,
                character.SubClass ?? "-",
                character.MaxHp ?? "-",
                character.ArmorClass ?? "-"
            );
        }
        AnsiConsole.Write(table);
    }
}