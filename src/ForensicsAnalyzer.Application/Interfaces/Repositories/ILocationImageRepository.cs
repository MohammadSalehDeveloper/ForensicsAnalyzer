using ForensicsAnalyzer.Domain.Entities;

namespace ForensicsAnalyzer.Application.Interfaces.Repositories;

public interface ILocationImageRepository
{
    Task AddAsync(LocationImage entity, CancellationToken ct);
    Task<LocationImage?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<IReadOnlyList<LocationImage>> GetByFileCustomIdAsync(Guid fileCustomId, CancellationToken ct);
    Task UpdateAsync(LocationImage entity, CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct);
}
