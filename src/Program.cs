using gm_codex.Application.Commands;
using Microsoft.Extensions.Configuration;
using gm_codex.Application.Services;
using gm_codex.Infrastructure.Data;
using gm_codex.Infrastructure.Repositories;
using gm_codex.Presentation.ConsoleUI;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;

// --------------------
// 1. Config setup
// --------------------
var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

// ------------------------
// 2. Services registration
// ------------------------

var services = new ServiceCollection();

// Register configuration
services.AddSingleton<IConfiguration>(config);

// Register infrastructure
services.AddSingleton<DbConnector>();
services.AddSingleton<EntityRepository>();
services.AddSingleton<EncounterRepository>();
services.AddSingleton<EncounterParticipantsRepository>();

// Register services
services.AddSingleton<CharacterService>();

// Register UI
services.AddSingleton<ConsoleUi>();

// ---------------------------
// 3. Initialize Database & UI
// ----------------------------

var serviceProvider = services.BuildServiceProvider();

var dbConnector = new DbConnector(config);
var entityRepository = new EntityRepository(dbConnector);
var encounterRepository = new EncounterRepository(dbConnector);
var encounterParticipantRepository = new EncounterParticipantsRepository(dbConnector);

var characterService = new CharacterService(entityRepository);

var entityRepository = serviceProvider.

// Ensure tables exist and render start screen
//entityRepository.CreateTable();
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
        list.AddCommand<ListPcCommand>("pc")
            .WithDescription("List all playable characters");
        list.AddCommand<ListNpcCommand>("npc")
            .WithDescription("List all non playable characters");
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
ListPcCommand.CharacterService = characterService;
ListNpcCommand.CharacterService = characterService;
ReadCommand.CharacterService = characterService;
DeleteCommand.CharacterService = characterService;
CreatePcCommand.CharacterService = characterService;
CreateNpcCommand.CharacterService = characterService;

return app.Run(args);