using gm_codex.Application.Commands;
using gm_codex.Application.Commands.Encounters;
using gm_codex.Application.Commands.Entities;
using Microsoft.Extensions.Configuration;
using gm_codex.Application.Services;
using gm_codex.Infrastructure.Data;
using gm_codex.Infrastructure.DependencyInjection;
using gm_codex.Infrastructure.Repositories;
using gm_codex.Infrastructure.Repositories.Interface;
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
services.AddScoped<DbConnector>();
services.AddScoped<EntityRepository>();
services.AddScoped<EncounterRepository>();
services.AddScoped<EncounterParticipantsRepository>();

// Register services (scoped per command execution)
services.AddScoped<CharacterService>();
services.AddScoped<EncounterService>();

// Register Interface
services.AddScoped<IEntityRepository, EntityRepository>();
services.AddScoped<IEncounterRepository, EncounterRepository>();

// Register UI
services.AddSingleton<ConsoleUi>();

// ---------------------------
// 3. Initialize Database & UI
// ----------------------------
// Create one temp scope to run on startup tasks (tables + start screen)
using (var provider = services.BuildServiceProvider())
using (var scope = provider.CreateScope())
{
    var entityRepo = scope.ServiceProvider.GetRequiredService<EntityRepository>();
    var encounterRepo = scope.ServiceProvider.GetRequiredService<EncounterRepository>();
    var encounterParticipantsRepository = scope.ServiceProvider.GetRequiredService<EncounterParticipantsRepository>();
    
    entityRepo.CreateTable();
    encounterParticipantsRepository.CreateTable();
    encounterRepo.CreateTable();
    
}

// --------------------
// 4. Setup CLI with DI
// --------------------
var registrar = new TypeRegistrar(services);
var app = new CommandApp(registrar);

app.Configure(configuration =>
{
    // Help
    configuration.AddCommand<HelpCommand>("help")
        .WithDescription("Show deatiled help with examples");

    // List branch
    configuration.AddBranch("list", list =>
    {
        list.SetDescription("List various game entities");
        list.AddCommand<ListPcCommand>("pc")
            .WithDescription("List all playable characters");
        list.AddCommand<ListNpcCommand>("npc")
            .WithDescription("List all non playable characters");
    });

    // Read branch
    configuration.AddBranch("read", read =>
    {
        read.SetDescription("Read various game entities");
        read.AddCommand<ReadCommand>("entity")
            .WithDescription("Read one entity");
    });

    // Delete branch
    configuration.AddBranch("delete", delete =>
    {
        delete.SetDescription("Delete various game entities");
        delete.AddCommand<DeletePcCommand>("pc")
            .WithDescription("Delete one playable character");
        delete.AddCommand<DeleteNpcCommand>("npc")
            .WithDescription("Delete one non-playable character");
    });

    // Create branch
    configuration.AddBranch("create", create =>
    {
        create.SetDescription("Create game entities and encounters");
        create.AddCommand<CreatePcCommand>("pc")
            .WithDescription("Create one playable character");
        create.AddCommand<CreateNpcCommand>("npc")
            .WithDescription("Create one non playable character");
        
        
        create.AddCommand<CreateEncounterCommand>("encounter")
            .WithDescription("Create one encounter");
    });
    
    // Update branch
    configuration.AddBranch("update", update =>
    {
        update.SetDescription("Update various game entities");
        update.AddCommand<UpdatePcCommand>("pc")
            .WithDescription("Update one entity");
        update.AddCommand<UpdateNpcCommand>("npc")
            .WithDescription("Update one non playable character");
    });
});

return app.Run(args);