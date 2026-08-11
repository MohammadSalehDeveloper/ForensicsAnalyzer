using ForensicsAnalyzer.Domain.Entities;

namespace ForensicsAnalyzer.Application.Interfaces.Repositories;

public interface ILocationRepository
{
    Task AddAsync(Location entity, CancellationToken ct);
    Task<Location?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<IReadOnlyList<Location>> GetByArtifactIdAsync(Guid artifactId, CancellationToken ct);
    Task UpdateAsync(Location entity, CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct);
}
