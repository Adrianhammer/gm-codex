using gm_codex.Application.Commands;
using gm_codex.Application.Commands.Encounters;
using gm_codex.Application.Commands.Entities;
using gm_codex.Application.Commands.Import;
using gm_codex.Application.Commands.Party;
using gm_codex.Application.Services;
using gm_codex.Infrastructure.Data;
using gm_codex.Presentation.DependencyInjection;
using gm_codex.Infrastructure.Integrations.Monsters;
using gm_codex.Infrastructure.Integrations.Open5e;
using gm_codex.Infrastructure.Interface;
using gm_codex.Infrastructure.Repositories;
using gm_codex.Infrastructure.Repositories.Interface;
using gm_codex.Application.ConsoleUI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;

// --------------------
// 1. Config setup
// --------------------
var config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
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
services.AddScoped<MonsterApiClient>();

// Register services (scoped per command execution)
services.AddScoped<EntityService>();
services.AddScoped<EncounterService>();
services.AddScoped<EncounterParticipantService>();
services.AddScoped<PartyService>();
services.AddScoped<ImportService>();

// Register Interface
services.AddScoped<IEntityRepository, EntityRepository>();
services.AddScoped<IEncounterRepository, EncounterRepository>();
services.AddScoped<IEncounterParticipantRepository, EncounterParticipantsRepository>();
services.AddScoped<IPartyRepository, PartyRepository>();
services.AddScoped<IPartyMemberRepository, PartyMemberRepository>();
services.AddScoped<IMonsterDataProvider, MonsterApiClient>();

// Register UI
services.AddSingleton<ConsoleUi>();

services.AddHttpClient<Open5EApiClient>(client =>
{
    client.BaseAddress = new Uri("https://api.open5e.com/");
    client.DefaultRequestHeaders.Accept.Add(
        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
    );
});

// ---------------------------
// 3. Initialize Database & UI
// ----------------------------
// Create one temp scope to run on startup tasks (tables + start screen)
using (var provider = services.BuildServiceProvider())
using (var scope = provider.CreateScope())
{
    var entityRepository = scope.ServiceProvider.GetRequiredService<EntityRepository>();
    var encounterRepository = scope.ServiceProvider.GetRequiredService<EncounterRepository>();
    var encounterParticipantsRepository =
        scope.ServiceProvider.GetRequiredService<EncounterParticipantsRepository>();
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
    configuration.AddHelpCommand();

    // List branch
    configuration.AddBranch(
        "list",
        list =>
        {
            list.SetDescription("List various game entities");

            list.AddListPcCommand();
            list.AddListNpcCommand();

            list.AddListEncounterCommand();

            list.AddListPartyCommand();

            list.AddListAllCommand();
        }
    );

    // Read branch
    configuration.AddBranch(
        "read",
        read =>
        {
            read.SetDescription("Read game entities, encounters and encounter participants");

            read.AddReadEntityCommand();

            read.AddReadEncounterCommand();

            read.AddReadPartyCommand();
        }
    );

    // Delete branch
    configuration.AddBranch(
        "delete",
        delete =>
        {
            delete.SetDescription("Delete various game entities");

            delete.AddDeletePcCommand();

            delete.AddDeleteNpcCommand();

            delete.AddDeleteEncounterCommand();
        }
    );

    // Create branch
    configuration.AddBranch(
        "create",
        create =>
        {
            create.SetDescription("Create game entities and encounters");

            create.AddCreatePcCommand();

            create.AddCreateNpcCommand();

            create.AddCreateEncounterCommand();

            create.AddCreatePartyCommand();
        }
    );

    // Update branch
    configuration.AddBranch(
        "update",
        update =>
        {
            update.SetDescription("Update various game entities");
            // Entities

            update.AddUpdatePcCommand();

            update.AddUpdateNpcCommand();

            update.AddUpdateEncounterCommand();
        }
    );

    // Import branch
    configuration.AddBranch(
        "import",
        import =>
        {
            import.SetDescription("Import various game entities");
            import.AddImportMonstersCommand();
        }
    );

    configuration.AddEncounterParticipantCommand();

    // Party section
    configuration.AddBranch(
        "party",
        party =>
        {
            party.SetDescription("Party commands");
            party.AddAddPartyMemberCommand();
            party.AddRemovePartyMemberCommand();
        }
    );
});

return app.Run(args);
