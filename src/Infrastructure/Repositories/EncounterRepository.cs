using Dapper;
using gm_codex.Application.Common;
using gm_codex.Domain.Models;
using gm_codex.Infrastructure.Data;
using gm_codex.Infrastructure.Repositories.Interface;

namespace gm_codex.Infrastructure.Repositories;

public class EncounterRepository : IEncounterRepository
{
    private readonly DbConnector _db;

    public EncounterRepository(DbConnector db)
    {
        _db = db;
    }

    public void CreateTable()
    {
        using var connection = _db.CreateConnection();
        connection.Open();
        
        var query = @"CREATE TABLE IF NOT EXISTS Encounters (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        Description TEXT
                )";
        
        connection.Execute(query);
    }

    public int InsertEncounter(Encounter encounter)
    {
        using var connection = _db.CreateConnection();
        connection.Open();
        
        EncounterRecord encounterRecord = new EncounterRecord
        {
            Name = encounter.Name,
            Description = encounter.Description,
        };
        
        var query = @"INSERT INTO Encounters (Name, Description) VALUES (@Name, @Description)";
        
        return connection.Execute(query, encounterRecord);
        
    }

    public EncounterRecord? GetEncounter(string name)
    {
        using var connection = _db.CreateConnection();
        connection.Open();
        
        var query = @"SELECT Id FROM Encounters WHERE Name = @Name";
        
        return connection.QuerySingleOrDefault<EncounterRecord>(query, new EncounterRecord { Name = name });
        
    }
}