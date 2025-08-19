using Spectre.Console;
using Spectre.Console.Cli;
using System.IO;
using gm_codex.Presentation.ConsoleUI;

namespace gm_codex.Application.Commands;

public class HelpCommand : Command
{
    public override int Execute(CommandContext context)
    {
        ConsoleUi.RenderHelpScreen();
        return 0;
    }
}