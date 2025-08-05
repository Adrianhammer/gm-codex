using Microsoft.Extensions.Configuration;
using gm_codex.ConsoleUI;
using gm_codex.Data;
using gm_codex.Services;
using Spectre.Console;

//Loading appsettings json into project
var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();
    
//Giving dbConnector class the db config
var dbConnector = new DbConnector(config);

ConsoleUi ui = new ConsoleUi();

ui.RenderStartScreen();

//Creating table
CharacterRepository characterRepository = new CharacterRepository(dbConnector);
characterRepository.CreateTable();

Console.WriteLine("Type 1 to create a new character");
var prompt = Console.ReadLine();
switch (prompt)
{
    case "1":
        CharacterService characterService = new CharacterService(characterRepository);
        characterService.CreateCharacter();
        break;
    case "2":
        AnsiConsole.Markup("[green]Functionality coming...[/]");
        break;
    default:
        AnsiConsole.Markup("[red]Functionality not recognized.[/]");
        break;
}


