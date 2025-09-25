using Dapper;
using gm_codex.Domain.Models;
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

    public int InsertMember(Entity entityId, Party partyId)
    {
        throw new NotImplementedException();
    }
    
    public int RemoveMember(Entity entityId, Party partyId) => throw new NotImplementedException();
    public PartyMemberRecord GetMemberByPartyId(int partyId) => throw new NotImplementedException();
}