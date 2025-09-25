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
using Microsoft.Extensions.DependencyInjection.Extensions;
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
services.AddScoped<EncounterParticipantsRepository>();
services.AddScoped<EntityRepository>();
services.AddScoped<EncounterRepository>();
services.AddScoped<EncounterParticipantsRepository>();
services.AddScoped<PartyRepository>();
services.AddScoped<PartyMemberRepository>();

// Register services (scoped per command execution)
services.AddScoped<EntityService>();
services.AddScoped<EncounterService>();
services.AddScoped<EncounterParticipantService>();

// Register Interface
services.AddScoped<IEntityRepository, EntityRepository>();
services.AddScoped<IEncounterRepository, EncounterRepository>();
services.AddScoped<IEncounterParticipantRepository, EncounterParticipantsRepository>();
services.AddScoped<IPartyRepository, PartyRepository>();
services.AddScoped<IPartyMemberRepository, PartyMemberRepository>();

// Register UI
services.AddSingleton<ConsoleUi>();

// ---------------------------
// 3. Initialize Database & UI
// ----------------------------
// Create one temp scope to run on startup tasks (tables + start screen)
using (var provider = services.BuildServiceProvider())
using (var scope = provider.CreateScope())
{
    var entityRepository = scope.ServiceProvider.GetRequiredService<EntityRepository>();
    var encounterRepository = scope.ServiceProvider.GetRequiredService<EncounterRepository>();
    var encounterParticipantsRepository = scope.ServiceProvider.GetRequiredService<EncounterParticipantsRepository>();
    var partyRepository = scope.ServiceProvider.GetRequiredService<PartyRepository>();
    var partyMemberRepository = scope.ServiceProvider.GetRequiredService<PartyMemberRepository>();
    
    entityRepository.CreateTable();
    encounterParticipantsRepository.CreateTable();
    encounterRepository.CreateTable();
    partyRepository.CreateTable();
    partyMemberRepository.CreateTable();
    
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
        .WithDescription("Show detailed help with examples");

    // List branch
    configuration.AddBranch("list", list =>
    {
        list.SetDescription("List various game entities");
        // Entities
        list.AddCommand<ListPcCommand>("pc")
            .WithDescription("List all playable characters");
        list.AddCommand<ListNpcCommand>("npc")
            .WithDescription("List all non playable characters");
        // Encounters
        list.AddCommand<ListEncounterCommand>("encounter")
            .WithDescription("List all encounters");
        // All
        list.AddCommand<ListAllCommand>("all")
            .WithDescription("List everything");
    });

    // Read branch
    configuration.AddBranch("read", read =>
    {
        read.SetDescription("Read game entities, encounters and encounter participants");
        // Entities
        read.AddCommand<ReadEntityCommand>("entity")
            .WithDescription("Read one entity");
        // Encounters
        read.AddCommand<ReadEncounterCommand>("encounter")
            .WithDescription("Get info on one encounter");
    });

    // Delete branch
    configuration.AddBranch("delete", delete =>
    {
        delete.SetDescription("Delete various game entities");
        // Entities
        delete.AddCommand<DeletePcCommand>("pc")
            .WithDescription("Delete one playable character");
        delete.AddCommand<DeleteNpcCommand>("npc")
            .WithDescription("Delete one non-playable character");
        // Encounters
        delete.AddCommand<DeleteEncounterCommand>("encounter")
            .WithDescription("Delete one encounter");
    });

    // Create branch
    configuration.AddBranch("create", create =>
    {
        create.SetDescription("Create game entities and encounters");
        // Entities
        create.AddCommand<CreatePcCommand>("pc")
            .WithDescription("Create one playable character");
        create.AddCommand<CreateNpcCommand>("npc")
            .WithDescription("Create one non playable character");
        // Encounters
        create.AddCommand<CreateEncounterCommand>("encounter")
            .WithDescription("Create one encounter");
    });
    
    // Update branch
    configuration.AddBranch("update", update =>
    {
        update.SetDescription("Update various game entities");
        // Entities
        update.AddCommand<UpdatePcCommand>("pc")
            .WithDescription("Update one entity");
        update.AddCommand<UpdateNpcCommand>("npc")
            .WithDescription("Update one non playable character");
        // Encounters
        update.AddCommand<UpdateEncounterCommand>("encounter")
            .WithDescription("Update one encounter");
    });
    
    // Encounter participant section
    configuration.AddCommand<EncounterParticipantCommand>("add")
        .WithDescription("Add entities to encounter");
});

return app.Run(args);