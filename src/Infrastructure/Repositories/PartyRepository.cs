using Dapper;
using gm_codex.Infrastructure.Data;
using gm_codex.Infrastructure.Repositories.Interface;

namespace gm_codex.Infrastructure.Repositories;

public class PartyRepository : IPartyRepository
{
    private readonly DbConnector _db;
    
    public PartyRepository(DbConnector db) =>  _db = db;

    public void CreateTable()
    {
        using var connection = _db.CreateConnection();
        connection.Open();

        var query = @"CREATE TABLE IF NOT EXISTS Parties (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        Description TEXT NOT NULL,
                        UNIQUE (Name))";
        
        connection.Execute(query);
    }

    public int InsertParty(String name, String description)
    {
        throw new NotImplementedException();
    }

    public PartyRecord GetParty(String name)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<PartyRecord?> GetParties()
    {
        throw new NotImplementedException();
    }
}