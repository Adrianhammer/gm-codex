using gm_codex.Application.Commands;
using Microsoft.Extensions.Configuration;
using gm_codex.Application.Services;
using gm_codex.Infrastructure.Data;
using gm_codex.Infrastructure.Repositories;
using gm_codex.Presentation.ConsoleUI;
using Spectre.Console.Cli;

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
// 2. Setup CLI
// ------------------
var app = new CommandApp();

app.Configure(configuration =>
{
    //Help
    configuration.AddCommand<HelpCommand>("help")
        .WithDescription("Show deatiled help with examples");

    //List branch
    configuration.AddBranch("list", list =>
    {
        list.SetDescription("List various game entities");
        list.AddCommand<ListPcsCommand>("pcs")
            .WithDescription("List all playable characters");
    });

    //Read branch
    configuration.AddBranch("read", read =>
    {
        read.SetDescription("Read various game entities");
        read.AddCommand<ReadCommand>("entity")
            .WithDescription("Read one entity");
    });

    //Delete branch
    configuration.AddBranch("delete", delete =>
    {
        delete.SetDescription("Delete various game entities");
        delete.AddCommand<DeleteCommand>("entity")
            .WithDescription("Delete one entity");
    });

    //Create branch
    configuration.AddBranch("create", create =>
    {
        create.SetDescription("Create various game entities");
        create.AddCommand<CreatePcCommand>("pc")
            .WithDescription("Create one playable character");
        create.AddCommand<CreateNpcCommand>("npc")
            .WithDescription("Create one non playable character");
    });
    
});



//Register services for dependency injection, uncomment later
/*
app.Configure(config =>
{
    config.Settings.Registrar.Register(typeof(CharacterService), characterService);
});
*/

//Temporary until we add Dependency injection
ListPcsCommand.CharacterService = characterService;
ReadCommand.CharacterService = characterService;
DeleteCommand.CharacterService = characterService;
CreatePcCommand.CharacterService = characterService;
CreateNpcCommand.CharacterService = characterService;

return app.Run(args);