using Dapper;
using gm_codex.Infrastructure.Data;
using gm_codex.Infrastructure.Repositories.Interface;

namespace gm_codex.Infrastructure.Repositories;

public class PartyMemberRepository : IPartyMemberRepository
{
    private readonly DbConnector _db;
    
    public PartyMemberRepository(DbConnector db) =>  _db = db;

    public void CreateTable()
    {
        using var connection = _db.CreateConnection();
        connection.Open();

        var query = @"CREATE TABLE IF NOT EXISTS PartyMember (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        PartyId INTEGER NOT NULL,
                        EntityId INTEGER NOT NULL,
                        FOREIGN KEY (partyId) REFERENCES Parties(Id),
                        FOREIGN KEY (entityId) REFERENCES Entities(Id),
                        UNIQUE(PartyId, EntityId)
                                       )";
        
        connection.Execute(query);
    }

    public int InsertMember(int partyId, int entityId)
    {
        using var connection = _db.CreateConnection();
        connection.Open();
        
        var query = @"INSERT INTO PartyMember (PartyId, EntityId) VALUES (@PartyId, @EntityId)";
        
        return connection.Execute(query, new { PartyId = partyId, EntityId = entityId });
    }

    public int RemoveMember(int partyId, int entityId)
    {
        using var connection = _db.CreateConnection();
        connection.Open();
        
        var query = @"DELETE FROM PartyMember WHERE PartyId = @PartyId AND EntityId = @EntityId";
        
        return connection.Execute(query, new { PartyId = partyId, EntityId = entityId });
    }

    public IEnumerable<PartyMemberRecord>? GetMemberByPartyId(int partyId)
    {
        using var connection = _db.CreateConnection();
        connection.Open();
        
        var query = @"SELECT * FROM PartyMember WHERE PartyId = @PartyId";
        
        return connection.Query<PartyMemberRecord>(query, new { PartyId = partyId });
    }
}