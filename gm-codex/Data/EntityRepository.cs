using Dapper;
using gm_codex.Models;

namespace gm_codex.Data;

public class EntityRepository
{
    private readonly DbConnector _db;

    public EntityRepository(DbConnector db)
    {
        _db = db;
    }

    public void CreateTable()
    {
        using var connection = _db.CreateConnection();
        connection.Open();
        
        var query = @"CREATE TABLE IF NOT EXISTS Entities (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        EntityType TEXT NOT NULL CHECK ( EntityType IN ('PC', 'NPC')),
                        Race TEXT NOT NULL,
                        SubRace TEXT,
                        CharacterClass TEXT NOT NULL,
                        SubClass TEXT,
                        MaxHp INTEGER,
                        ArmorClass Integer
            )";
        
        connection.Execute(query);
    }
    
    public void InsertEntity(Character character)
    {
           using var connection = _db.CreateConnection();
           connection.Open();

           var characterEntity = new CharacterEntity
           {
               Name = character.Name,
               EntityType = character.EntityType,
               Race = character.Race.ToString(),
               SubRace = character.SubRace,
               CharacterClass = character.CharacterClass.ToString(),
               SubClass = character.SubClass
           };

           var query = @"INSERT INTO Entities (Name, Race, EntityType, SubRace, CharacterClass, SubClass) 
                         VALUES (@Name, @Race, @EntityType, @SubRace, @CharacterClass, @SubClass)";
           
           connection.Execute(query, characterEntity);
           
    }
}