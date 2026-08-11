using ForensicsAnalyzer.Domain.Entities;

namespace ForensicsAnalyzer.Application.Interfaces.Repositories;

public interface IArtifactItemRepository
{
    Task AddAsync(ArtifactItem entity, CancellationToken ct);
    Task<ArtifactItem?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<IReadOnlyList<ArtifactItem>> GetByArtifactIdAsync(Guid artifactId, CancellationToken ct);
    Task UpdateAsync(ArtifactItem entity, CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct);
}
