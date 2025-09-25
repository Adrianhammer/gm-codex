using Dapper;
using gm_codex.Domain.Models;
using gm_codex.Infrastructure.Data;
using gm_codex.Infrastructure.Data.Mappers;
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

    public int InsertParty(Party party)
    {
        using var connection = _db.CreateConnection();
        connection.Open();
        
        var query = @"INSERT INTO Parties (Name, Description) VALUES (@Name, @Description)";
        
        return connection.Execute(query, PartyMapper.ToRecord(party));
    }

    public PartyRecord? GetParty(String name)
    {
        using var connection = _db.CreateConnection();
        connection.Open();
        
        var query = @"SELECT * FROM Parties WHERE Name = @Name";
        
        return connection.QuerySingleOrDefault<PartyRecord>(query, new { Name = name });
    }

    public IEnumerable<PartyRecord?> GetParties()
    {
        throw new NotImplementedException();
    }
}