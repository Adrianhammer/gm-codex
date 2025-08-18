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

var rootCommand = new RootCommand("gm-codex CLI - Manage your TTRPG encounters");

//list-pcs
var listPcsCommand = new Command("list-pcs", "list all playable characters");
rootCommand.Add(listPcsCommand);

//parse manually
var parseResult = rootCommand.Parse(args);

if (parseResult.Tokens.Count > 0 && parseResult.Tokens[0].Value == "list-pcs")
{
    characterService.ListPlayableCharacters();
    return 0;
}

foreach (var error in parseResult.Errors)
{
    Console.Error.WriteLine(error.Message);
}

return 1;
