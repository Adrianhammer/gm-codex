using gm_codex.Application.Common;
using Spectre.Console;

namespace gm_codex.Presentation.ConsoleUI.Entities;

public class DeleteEntityUi
{
    public static void ViewDeleteEntity(Result<int> result,  string name)
    {

        if (!result.Success)
        {
            AnsiConsole.MarkupLine($"[yellow]Warning[/]: {result.Error} '{name}'");
            return;
        }
        
        AnsiConsole.MarkupLine($"[green]Success[/]: Entity '{name}' has been deleted.");
        
    }
}