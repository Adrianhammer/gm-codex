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
                        EntityClass TEXT NOT NULL,
                        SubClass TEXT,
                        MaxHp INTEGER,
                        ArmorClass Integer
                    )";
        
        connection.Execute(query);
    }
    
    public void InsertEntity(Entity entity)
    {
           using var connection = _db.CreateConnection();
           connection.Open();

           var characterEntity = new EntityRecord
           {
               Name = entity.Name,
               EntityType = entity.EntityType,
               Race = entity.Race.ToString(),
               SubRace = entity.SubRace,
               EntityClass = entity.EntityClass.ToString(),
               SubClass = entity.SubClass,
               MaxHp = entity.MaxHp,
               ArmorClass = entity.ArmorClass,
           };

           var query = @"INSERT INTO Entities (Name, EntityType, Race, SubRace, EntityClass, SubClass, MaxHp, ArmorClass) 
                         VALUES (@Name, @EntityType, @Race, @SubRace, @EntityClass, @SubClass, @MaxHp, @ArmorClass);";
           
           connection.Execute(query, characterEntity);
           
    }
}