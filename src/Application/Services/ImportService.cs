using gm_codex.Application.Common;
using gm_codex.Domain.Models;
using gm_codex.Infrastructure.Data;
using gm_codex.Infrastructure.Data.Mappers;
using gm_codex.Infrastructure.Interface;
using gm_codex.Infrastructure.Repositories.Interface;

namespace gm_codex.Application.Services;

public class ImportService
{
    private readonly IEntityRepository _repository;
    private readonly IMonsterDataProvider _monsterDataProvider;

    public ImportService(IEntityRepository repository, IMonsterDataProvider monsterDataProvider)
    {
        _repository = repository;  
        _monsterDataProvider = monsterDataProvider;
    } 

    public Result<int> ImportAllMonsters()
    {
        var importedEntities = new List<EntityRecord>();

        try
        {
            // Check if imported before

            var npc = _monsterDataProvider.GetAllMonstersAsync();
            
            foreach (var monster in npc.Result)
            {
                importedEntities.Add(EntityMapper.ToRecord(monster));
            }
            
            var rows = _repository.ImportEntities(importedEntities);
            
            return rows > 0
                ? Result<int>.Ok(rows) 
                : Result<int>.Fail("Error importing monsters");
        }
        catch (Exception e)
        {
            return Result<int>.Fail(e.Message);
        }
    }
}