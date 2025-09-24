using gm_codex.Application.Common;
using gm_codex.Infrastructure.Data;
using gm_codex.Presentation.ConsoleUI.Encounters;
using gm_codex.Presentation.ConsoleUI.Entities;

namespace gm_codex.Presentation.ConsoleUI;

public class ListAllUi
{
    public static void ViewAll(Result<List<EncounterRecord>> encounterResult, Result<List<EntityRecord>> npcResult, Result<List<EntityRecord>> pcResult )
    {
        ReadEncounterUi.ViewAllEncounters(encounterResult);
        ReadPcUi.ViewEntitiesByType(pcResult);
        ReadPcUi.ViewEntitiesByType(npcResult);
    }
}