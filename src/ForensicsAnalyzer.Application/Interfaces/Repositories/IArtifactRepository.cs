using ForensicsAnalyzer.Domain.Entities;

namespace ForensicsAnalyzer.Application.Interfaces.Repositories;

public interface IArtifactRepository
{
    Task AddAsync(Artifact entity, CancellationToken ct);
    Task<Artifact?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<IReadOnlyList<Artifact>> GetAllAsync(CancellationToken ct);
    Task<IReadOnlyList<Artifact>> GetByCaseIdAsync(Guid caseId, CancellationToken ct);
    Task UpdateAsync(Artifact entity, CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct);
}
