using gm_codex.Application.Common;
using gm_codex.Infrastructure.Repositories.Interface;

namespace gm_codex.Application.Services;

public class ImportService
{
    private readonly IEntityRepository _repository;
    
    public ImportService(IEntityRepository repository) => _repository = repository;

    public Result<int> ImportAllMonsters()
    {
        // Check if imported before
        // Call C# method in future Open5E that calls api and pulls down monsters
        // Map entities to domain or records
        // Call repo method and send the imported monsters there for db import
        throw new NotImplementedException();
    }
}