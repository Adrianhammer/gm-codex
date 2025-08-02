using Microsoft.Extensions.Configuration;
using gm_codex.ConsoleUI;
using gm_codex.Data;

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
characterRepository.CreateTable(dbConnector);

//lager enkel prompt her for å trigge en funksjon - gjøres om senere bare for testing
Console.WriteLine("Type 1 to create a character: ");
string prompt = Console.ReadLine();
