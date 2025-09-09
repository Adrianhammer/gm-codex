using Spectre.Console;

namespace gm_codex.Presentation.ConsoleUI;

public class DeleteEntityUi
{
    public static void ViewDeleteEntity(string entityName)
    {
        AnsiConsole.MarkupLine($"[bold]{entityName}[/] is now deleted from the database.");
    }
}