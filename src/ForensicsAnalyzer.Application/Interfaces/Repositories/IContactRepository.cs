using ForensicsAnalyzer.Domain.Entities;

namespace ForensicsAnalyzer.Application.Interfaces.Repositories;

public interface IContactRepository
{
    Task AddAsync(Contact entity, CancellationToken ct);
    Task<Contact?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<IReadOnlyList<Contact>> GetByArtifactIdAsync(Guid artifactId, CancellationToken ct);
    Task UpdateAsync(Contact entity, CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct);
}
