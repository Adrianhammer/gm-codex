using System.Data.Common;
using Dapper;
using gm_codex.Models;
using Microsoft.Data.Sqlite;

namespace gm_codex.Data;

public class CharacterRepository
{
    private readonly DbConnector _db;

    public CharacterRepository(DbConnector db)
    {
        _db = db;
    }

    public void CreateTable()
    {
        using var connection = _db.CreateConnection();
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
    
    public void InsertCharacter(Character character)
    {
           using var connection = _db.CreateConnection();
           connection.Open();

           var query = @"INSERT INTO Characters (Name, Race, SubRace, CharacterClass, SubClass) 
                         VALUES (@Name, @Race, @SubRace, @CharacterClass, @SubClass)";
           
           connection.Execute(query, character);
           
    }
}