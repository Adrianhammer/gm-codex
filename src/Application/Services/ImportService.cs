using System.Collections;
using gm_codex.Application.Common;
using gm_codex.Domain.Models;
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

    public Result<IEnumerable<Entity>> ImportAllMonsters()
    {
        // Check if imported before

        Console.WriteLine("Now in Service class");
        var npc = _monsterDataProvider.GetAllMonstersAsync();

        // Call repo method and send the imported monsters there for db import
        var rows = _repository.ImportEntities();
        return Result<IEnumerable<Entity>>.Ok(npc.Result);
    }
}