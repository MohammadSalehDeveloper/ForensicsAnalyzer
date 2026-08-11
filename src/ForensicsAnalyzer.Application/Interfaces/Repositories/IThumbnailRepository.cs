using ForensicsAnalyzer.Domain.Entities;

namespace ForensicsAnalyzer.Application.Interfaces.Repositories;

public interface IThumbnailRepository
{
    Task AddAsync(Thumbnail entity, CancellationToken ct);
    Task<Thumbnail?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<IReadOnlyList<Thumbnail>> GetByFileCustomIdAsync(Guid fileCustomId, CancellationToken ct);
    Task UpdateAsync(Thumbnail entity, CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct);
}
