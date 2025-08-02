using System.Data.Common;
using Dapper;
using gm_codex.Models;

namespace gm_codex.Data;

public class CharacterRepository
{
    private readonly DbConnector _db;

    public CharacterRepository(DbConnector db)
    {
        _db = db;
    }

    public void CreateTable(DbConnector db)
    {
        using var connection = db.CreateConnection();
        connection.Open();
        
        var query = @"CREATE TABLE IF NOT EXISTS Characters (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        Race TEXT NOT NULL,
                        SubRace TEXT,
                        CharacterClass TEXT NOT NULL,
                        SubClass TEXT
            )";
        
        connection.Execute(query);
    }

}