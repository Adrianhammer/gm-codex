using Dapper;
using gm_codex.Domain.Models;
using gm_codex.Infrastructure.Data;
using gm_codex.Infrastructure.Repositories.Interface;

namespace gm_codex.Infrastructure.Repositories;

public class EncounterParticipantsRepository : IEncounterParticipantRepository
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

    public EncounterParticipantRecord? GetEncounterIdByName(string encounterName)
    {
        Console.WriteLine("You reached repository: GetEncounterIdByName method");
        Console.WriteLine(encounterName);
        var enc = new EncounterParticipantRecord
        {
            DisplayName = encounterName,
        };
        return enc;
    }

    public IEnumerable<EncounterParticipantRecord> GetEncounterById(int encounterId)
    {
        using var connection = _db.CreateConnection();
        connection.Open();
        
        var query = @"SELECT * FROM EncounterParticipants WHERE EncounterId = @EncounterId";
        
        return connection.Query<EncounterParticipantRecord>(query, new EncounterParticipantRecord { EncounterId = encounterId });
    }

    public int GetParticipantCount(int encounterId, int entityId)
    {
        using var connection = _db.CreateConnection();
        connection.Open();

        var query = "SELECT COUNT(*) FROM EncounterParticipants WHERE EncounterId = @EncounterId AND EntityId = @EntityId";
        
        return connection.ExecuteScalar<int>(query, new EncounterParticipantRecord { EncounterId = encounterId , EntityId = entityId });
    }

    public int InsertParticipant(EncounterParticipant participant)
    {
        using var connection = _db.CreateConnection();
        connection.Open();

        EncounterParticipantRecord participantRecord = new EncounterParticipantRecord
        {
            EncounterId = participant.EncounterId,
            EntityId = participant.EntityId,
            DisplayName = participant.Name,
            CurrentHp = participant.CurrentHp,
            Initiative = participant.Initiative,
            Conditions = participant.Conditions,
        };

        var query =
            @"INSERT INTO EncounterParticipants (EncounterId, EntityId, DisplayName, CurrentHp, Initiative, Conditions) 
              VALUES (@EncounterId, @EntityId, @DisplayName, @CurrentHp, @Initiative, @Conditions)";
        
        return connection.Execute(query, participantRecord);
    }
}