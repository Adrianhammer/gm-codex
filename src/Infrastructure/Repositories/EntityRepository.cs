using Dapper;
using gm_codex.Domain.Enums;
using gm_codex.Domain.Models;
using gm_codex.Infrastructure.Data;
using gm_codex.Infrastructure.Repositories.Interface;

namespace gm_codex.Infrastructure.Repositories;

public class EntityRepository : IEntityRepository
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
                        EntityType TEXT NOT NULL CHECK ( EntityType IN ('pc', 'npc')),
                        Race TEXT NOT NULL,
                        SubRace TEXT,
                        EntityClass TEXT NOT NULL,
                        SubClass TEXT,
                        MaxHp INTEGER,
                        ArmorClass Integer
                    )";
        
        connection.Execute(query);
    }
    
    public int InsertEntity(Entity entity)
    {
           using var connection = _db.CreateConnection();
           connection.Open();

           var characterEntity = new EntityRecord
           {
               Name = entity.Name,
               EntityType = entity.EntityType.ToString(),
               Race = entity.Race.ToString(),
               SubRace = entity.SubRace,
               EntityClass = entity.EntityClass.ToString(),
               SubClass = entity.SubClass,
               MaxHp = entity.MaxHp,
               ArmorClass = entity.ArmorClass,
           };

           var query = @"INSERT INTO Entities (Name, EntityType, Race, SubRace, EntityClass, SubClass, MaxHp, ArmorClass) 
                         VALUES (@Name, @EntityType, @Race, @SubRace, @EntityClass, @SubClass, @MaxHp, @ArmorClass);";
           
           return connection.Execute(query, characterEntity);
    }

    public int UpdateEntity(Entity entity)
    {
        using var connection = _db.CreateConnection();
        connection.Open();

        var characterEntity = new EntityRecord()
        {
            Id = entity.Id,
            Name = entity.Name,
            EntityType = entity.EntityType.ToString(),
            Race = entity.Race.ToString(),
            SubRace = entity.SubRace,
            EntityClass = entity.EntityClass.ToString(),
            SubClass = entity.SubClass,
            MaxHp = entity.MaxHp,
            ArmorClass = entity.ArmorClass,
        };

        var query = @"
            UPDATE Entities
            SET Name = @Name,
                Race = @Race,
                SubRace = @SubRace,
                EntityClass = @EntityClass,
                SubClass = @SubClass,
                MaxHp = @MaxHp,
                ArmorClass = @ArmorClass
                WHERE Id = @Id
                AND EntityType = @EntityType;";
        
        return connection.Execute(query, characterEntity);
    }

    public EntityRecord? GetEntityByName(string name, EntityType entityType)
    {
        using var connection = _db.CreateConnection();
        connection.Open();

        var query = @"
            SELECT Id, Name, EntityType, Race, SubRace, EntityClass, SubClass, MaxHp, ArmorClass
            FROM Entities
            WHERE EntityType = @EntityType
                AND Name = @Name;";
        
        return connection.QuerySingleOrDefault<EntityRecord>(query, new
        {
            Name = name,
            EntityType = entityType.ToString().ToLower()
        });
    }

    public int DeleteEntityByName(string name, EntityType entityType)
    {
        using var connection = _db.CreateConnection();
        connection.Open();
        
        var query = @"DELETE FROM Entities WHERE EntityType = @EntityType AND Name = @Name;";

        return connection.Execute(query, new
        {
            Name = name,
            EntityType = entityType.ToString().ToLower()
        });
    }

    public IEnumerable<EntityRecord> GetAllPlayableCharacters()
    {
        using var connection = _db.CreateConnection();
        connection.Open();
        
        var query = @"SELECT * FROM Entities WHERE EntityType = 'pc'";

        return connection.Query<EntityRecord>(query).ToList();
    }
    
    public IEnumerable<EntityRecord> GetAllNonPlayableCharacters()
    {
        using var connection = _db.CreateConnection();
        connection.Open();
        
        var query = @"SELECT * FROM Entities WHERE EntityType = 'npc'";
        
        return connection.Query<EntityRecord>(query).ToList();
    }
}