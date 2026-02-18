using Spectre.Console;
namespace gm_codex.Application.ConsoleUI;

public class ConsoleUi
{
    public static void RenderHelpScreen()
    {
        var font = FigletFont.Load(
            Path.Combine(AppContext.BaseDirectory, "Resources", "Fonts", "Delta Corps Priest 1.flf")
        );
        
        // Title
        AnsiConsole.Write(
            new FigletText(font,"GM CODEX")
                .Centered()
                .Color(Color.IndianRed)
        );

        // ───────────────────────
        // Core Commands
        // ───────────────────────
        var core = new Table().HideHeaders().Border(TableBorder.None).AddColumns("Command", "Description");
        core.AddRow("[gold1]gm help[/]", "📖 Show this help screen");

        AnsiConsole.Write(new Panel(core)
            .Header("[bold yellow]📖 Core Commands[/]")
            .Border(BoxBorder.Rounded)
            .Expand());

        // ───────────────────────
        // Entity Management
        // ───────────────────────
        var entity = new Table().HideHeaders().Border(TableBorder.None).AddColumns("Command", "Description");
        entity.AddRow("[gold1]gm create pc[/]",
            "👤 Create a playable character\n   Syntax: gm create pc -n <name> -r <race> -c <class> | optional: --subclass -h <hp> -a <ac>");
        entity.AddRow("[gold1]gm create npc[/]",
            "👾 Create a non-playable character\n   Syntax: gm create npc -n <name> -r <race> -c <class> -h <hp> -a <ac>");
        entity.AddRow("[gold1]gm list pc/npc[/]", "👤 List all player characters or NPCs");
        entity.AddRow("[gold1]gm list all[/]", "👾 List everything");
        entity.AddRow("[gold1]gm delete <name>[/]", "🗑️ Delete entity by name");
        entity.AddRow("[gold1]gm import monsters[/]", "🗑️ Import over 3000 monsters from Open5E");

        AnsiConsole.Write(new Panel(entity)
            .Header("[bold yellow]👤 Entity Management[/]")
            .Border(BoxBorder.Rounded)
            .Expand());

        // ───────────────────────
        // Encounter Management
        // ───────────────────────
        var encounter = new Table().HideHeaders().Border(TableBorder.None).AddColumns("Command", "Description");
        encounter.AddRow("[gold1]gm create encounter[/]",
            "🗡️ Create a new encounter\n Syntax: gm create encounter -n <name> -d <description> \n");
        encounter.AddRow("[gold1]gm list encounters[/]", "🗡️ List all encounters");
        encounter.AddRow("[gold1]gm add -npc goblin:3 troll:1 -e <encounter>[/]",
            "🗡️ Add NPCs to an encounter\n Syntax: gm add -n goblin:3 -n troll:1 -e Goblin_Ambush");

        AnsiConsole.Write(new Panel(encounter)
            .Header("[bold yellow]🗡️ Encounter Management[/]")
            .Border(BoxBorder.Rounded)
            .Expand());

        // ───────────────────────
        // Global Options
        // ───────────────────────
        var options = new Table().Border(TableBorder.Rounded);
        options.AddColumn("[yellow]Flag[/]").AddColumn("Description");
        options.AddRow("-n | --name", "Entity or encounter name");
        options.AddRow("-r | --race", "Race of the entity");
        options.AddRow("-c | --class", "Class of the entity");
        options.AddRow("-e | --encounter", "Encounter name");
        options.AddRow("-h | --health", "Health Points (HP)");
        options.AddRow("-a | --armor", "Armor Class (AC)");
        options.AddRow("--subclass", "Subclass of the entity");
        options.AddRow("--subrace", "Subrace of the entity");


        AnsiConsole.Write(new Panel(options)
            .Header("[bold yellow]🪄 Global Options[/]")
            .Border(BoxBorder.Rounded)
            .Expand());

        // ───────────────────────
        // Quick Examples
        // ───────────────────────
        var examples = @"[green]gm create pc -n Astarion -r Elf -c Ranger[/]
[green]gm create npc -n Goblin -r Goblinoid -c barbarian -h 7 -a 13[/]
[green]gm create encounter -n Goblin Ambush -d Goblins leap from the trees![/]
[green]gm add -n goblin:3 -n troll:1 -e Goblin Ambush[/]";

        AnsiConsole.Write(new Panel(examples)
            .Header("[bold red]🔥 Quick Examples[/]")
            .Border(BoxBorder.Rounded)
            .Expand());
    }
}
