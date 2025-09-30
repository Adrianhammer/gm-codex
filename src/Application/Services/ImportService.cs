using gm_codex.Application.Common;
using gm_codex.Infrastructure.Repositories.Interface;

namespace gm_codex.Application.Services;

public class ImportService
{
    private readonly IEntityRepository _repository;
    
    public ImportService(IEntityRepository repository) => _repository = repository;

    public Result<int> ImportAllMonsters()
    {
        throw new NotImplementedException();
    }
}