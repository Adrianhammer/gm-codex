using Dapper;
using gm_codex.Infrastructure.Data;

namespace gm_codex.Infrastructure.Repositories;

public class EncounterParticipantsRepository
{
    private readonly DbConnector _db;

    public EncounterParticipantsRepository(DbConnector db)
    {
        _db = db;
    }

    public void CreateTable()
    {
        using var connection = _db.CreateConnection();
        connection.Open();
        
        var query = @"CREATE TABLE IF NOT EXISTS EncounterParticipants (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        EncounterId INTEGER NOT NULL,
                        EntityId INTEGER NOT NULL,
                        DisplayName TEXT NOT NULL,
                        CurrentHp INTEGER,
                        Initiative INTEGER,
                        Conditions TEXT,
                        FOREIGN KEY (EncounterId) REFERENCES Encounters(Id),
                        FOREIGN KEY (EntityId) REFERENCES Entities(Id)
                    )";
        
        connection.Execute(query);
    }
}