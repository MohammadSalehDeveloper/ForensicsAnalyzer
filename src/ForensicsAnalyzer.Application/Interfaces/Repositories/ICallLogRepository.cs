using ForensicsAnalyzer.Domain.Entities;

namespace ForensicsAnalyzer.Application.Interfaces.Repositories;

public interface ICallLogRepository
{
    Task AddAsync(CallLog entity, CancellationToken ct);
    Task<CallLog?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<IReadOnlyList<CallLog>> GetByArtifactIdAsync(Guid artifactId, CancellationToken ct);
    Task UpdateAsync(CallLog entity, CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct);
}
