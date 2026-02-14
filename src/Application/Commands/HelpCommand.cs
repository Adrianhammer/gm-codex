using gm_codex.Presentation.ConsoleUI;
using Spectre.Console.Cli;

namespace gm_codex.Application.Commands;

public class HelpCommand : Command
{
    public override int Execute(CommandContext context)
    {
        ConsoleUi.RenderHelpScreen();
        return 0;
    }
}

public static class HelpCommandExtensions
{
    public static void AddHelpCommand(this IConfigurator configuration)
    {
        configuration
            .AddCommand<HelpCommand>("help")
            .WithDescription("Show detailed help with examples");
    }
}

