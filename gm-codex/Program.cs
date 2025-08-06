using Microsoft.Extensions.Configuration;
using gm_codex.ConsoleUI;
using gm_codex.Data;
using gm_codex.Services;
using Spectre.Console;


var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();
        
var dbConnector = new DbConnector(config);
var characterRepository = new CharacterRepository(dbConnector);
var characterService = new CharacterService(characterRepository);

ConsoleUi ui = new ConsoleUi();
ui.RenderStartScreen();

while (true)
{
    var input = Console.ReadLine()?.Trim().ToLower();

    switch (input)
    {
        case "create-character":
            characterService.CreateCharacter();
            break;
        case "exit":
            AnsiConsole.MarkupLine("[yellow]Goodbye[/] :waving_hand:");
            return 0;
        default:
            AnsiConsole.MarkupLine("[red]Unknown command[/]");
            break;
    }
    Console.WriteLine();
}
