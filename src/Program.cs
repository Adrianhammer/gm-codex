using System;
using System.Collections.Generic;
using System.IO;
using System.CommandLine;
using System.CommandLine.Parsing;
using Microsoft.Extensions.Configuration;
using Spectre.Console;
using gm_codex.Application.Services;
using gm_codex.Infrastructure.Data;
using gm_codex.Infrastructure.Repositories;
using gm_codex.Presentation.ConsoleUI;
// --------------------
// 1. Config & Services
// --------------------
var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var dbConnector = new DbConnector(config);
var entityRepository = new EntityRepository(dbConnector);
var encounterRepository = new EncounterRepository(dbConnector);
var encounterParticipantRepository = new EncounterParticipantsRepository(dbConnector);

var characterService = new CharacterService(entityRepository);

// Ensure tables exist and render start screen
entityRepository.CreateTable();
encounterRepository.CreateTable();
encounterParticipantRepository.CreateTable();

ConsoleUi ui = new ConsoleUi();
ui.RenderStartScreen();

// ------------------
// 2. Define commands
// ------------------

var commands = new Dictionary<string, Action>()
{
    ["create-character"] = () => characterService.CreateEntity(),
    ["read-entity"] = () => characterService.ReadEntity(),
    ["delete-entity"] = () => characterService.DeleteEntity(),
    ["list-pcs"] = () => characterService.ListPlayableCharacters(),
    ["help"] = () => AnsiConsole.MarkupLine("[yellow]Available commands: create-character, read-character, help, exit[/]"),
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