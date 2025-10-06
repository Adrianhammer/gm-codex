using gm_codex.Application.Common;
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

    public async Task<Result<int>> ImportAllMonstersAsync()
    {
        try
        {
            // Implement later: Check if imported before

            var importedEntities = new List<EntityRecord>();
            var importResult = await _monsterDataProvider.GetAllMonstersAsync();

            foreach (var monster in importResult)
            {
                importedEntities.Add(EntityMapper.ToRecord(monster));
            }
            
            var rows = await _repository.ImportEntitiesAsync(importedEntities);
            
            return rows > 0
                ? Result<int>.Ok(rows) 
                : Result<int>.Fail("Something went wrong"); 
            
        }
        catch (Exception e)
        {
            return Result<int>.Fail(e.Message);
        }
    }
}