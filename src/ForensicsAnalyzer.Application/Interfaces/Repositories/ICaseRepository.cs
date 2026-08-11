using ForensicsAnalyzer.Domain.Entities;

namespace ForensicsAnalyzer.Application.Interfaces.Repositories;

public interface ICaseRepository
{
    Task AddAsync(Case entity, CancellationToken ct);
    Task<Case?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<IReadOnlyList<Case>> GetAllAsync(CancellationToken ct);
}
