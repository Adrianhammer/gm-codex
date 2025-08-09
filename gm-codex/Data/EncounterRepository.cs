using Dapper;

namespace gm_codex.Data;

public class EncounterRepository
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
}