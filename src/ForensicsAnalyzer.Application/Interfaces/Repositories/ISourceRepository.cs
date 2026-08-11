using ForensicsAnalyzer.Domain.Entities;

namespace ForensicsAnalyzer.Application.Interfaces.Repositories;

public interface ISourceRepository
{
    Task AddAsync(Source entity, CancellationToken ct);
    Task<Source?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<IReadOnlyList<Source>> GetAllAsync(CancellationToken ct);
    Task UpdateAsync(Source entity, CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct);
}
