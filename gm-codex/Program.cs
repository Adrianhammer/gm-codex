using System;
using System.Collections.Generic;
using System.IO;
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
var entityRepository = new EntityRepository(dbConnector);
var characterService = new CharacterService(entityRepository);
entityRepository.CreateTable();

ConsoleUi ui = new ConsoleUi();
ui.RenderStartScreen();

var commands = new Dictionary<string, Action>()
{
    ["create-character"] = () => characterService.CreateEntity(),
    ["help"] = () => AnsiConsole.MarkupLine("[yellow]Available commands: create-character, help, exit[/]"),
};

AnsiConsole.MarkupLine("[green]Welcome! :mage:[/]");

while (true)
{
    var input = AnsiConsole.Ask<string>(">")
        .Trim().ToLower();
    Console.WriteLine();

    if (input == "exit")
    {
        AnsiConsole.MarkupLine("[yellow]Goodbye[/] :waving_hand:");
        break;
    }
    if (commands.ContainsKey(input))
    {
        commands[input]();
    }
    else
    {
        AnsiConsole.MarkupLine("[red]Unknown command: {invalid}. Type 'help' for a list of available commands.[/]");
    }
    
    Console.WriteLine();
}
